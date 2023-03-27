# import-statements.
import os                                       as os
import re                                       as re
import datetime                                 as datetime
import torch                                    as torch
import torchvision                              as torchvision
import torchvision.models.detection             as tvdetection
import torchvision.models.detection.faster_rcnn as frrcnn
import PIL.Image                                as pilimage

import evaluator.pennfudan_dataset_evaluator    as pfde

class PennFudanDataset(torch.utils.data.Dataset):
    def __init__(
        self                    ,
        root_path          : str,
        annotations_folder : str,
        images_folder      : str,
        image_transform         ,
        target_transform
    ):
        self.root_path          : str = root_path

        self.annotations_folder : str = annotations_folder
        self.images_folder      : str = images_folder

        self.annotations : list = list(sorted(os.listdir(
            os.path.join(root_path, annotations_folder)
        )))
        self.images      : list = list(sorted(os.listdir(
            os.path.join(root_path, images_folder     )
        )))

        self.image_transform  = image_transform
        self.target_transform = target_transform

    def __len__(self):
        return len(self.images)

    def __getitem__(self, index):
        image_path      : str = os.path.join(
            self.root_path    ,
            self.images_folder,
            self.images[index]
        )
        annotation_path : str = os.path.join(
            self.root_path         ,
            self.annotations_folder,
            self.annotations[index]
        )

        # Be default, the image is expected to be in BGR mode.
        image = pilimage.open(image_path).convert("RGB")

        # The Target parameters for the Annotation dictionary.
        boxes  : list = [] # First as list, then to Tensor.
        labels : list = [] # First as list, then to Tensor.
        # Potentially, I need to specify image_id, area, and iscrowd.
        # I left it out, for experimentation purposes.


        # Time to parse the annotation file, supposedly in TXT format.
        annotation : dict = {}
        with open(annotation_path) as stream:
            for line in stream:
                if len(line) >= 5: # The "2" in "5" is to avoid "\n" characters.
                                   # But, why have the other "3"?
                    if line.rstrip()[0] != "#":
                        annotation[line.split(" : ")[0]] = line.split(" : ")[1]

        for key, value in annotation.items():
            match       = re.search(
                r"^Bounding box for object (\d) (.*)" +
                r" \(Xmin, Ymin\) - \(Xmax, Ymax\)$", key
            )
            match_value = re.findall(
                r"\d+", value
            ) # Searching for Xmin, Ymin, Xmax, Ymax numbers.

            if (match != None) and (match_value != None):
                # I hope, it should always end up being a list at this point.
                boxes.append([int(x) for x in match_value])

                # "1" should point to an object id.
                labels.append(int(match.group(1)))

        boxes  : torch.Tensor = torch.as_tensor(boxes     , dtype=torch.float32)
        labels : torch.Tensor = torch.ones((len(labels), ), dtype=torch.int64)

        target : dict = {}
        target["boxes"   ] = boxes
        target["labels"  ] = labels

        if self.image_transform:
            image = self.image_transform(image)

        if self.target_transform:
            target = self.target_transform(target)

        return image, target


def architecture(num_classes : int):
    # Load a pre-trained model.
    weights = tvdetection.FasterRCNN_ResNet50_FPN_Weights.COCO_V1
    model   = tvdetection.fasterrcnn_resnet50_fpn(weights=weights)

    # Now, we are trying to update the number of classes to
    # match our custom dataset, whatever it would be. But,
    # first, we need to make sure that the number of input
    # features will persist during the update process. And,
    # then, and only then, we set new number of classes.
    in_features                   = (
        model.roi_heads.box_predictor.cls_score.in_features
    )
    model.roi_heads.box_predictor = frrcnn.FastRCNNPredictor(
        in_features, num_classes
    )

    return model

def train(model, optimizer, dataloader, device):
    model.train()

    for batch_images, batch_targets in dataloader:
        # Load entire batch of images to the SELECTED device.
        images = [image.to(device) for image in batch_images]

        # The same for targets, i.e. list of dictionaries.
        targets = [
            {key: value.to(device) for key, value in t.items()}
            for t in batch_targets
        ] # Multiline to fit under 80 characters.

        # Actual training, I assume, the inference.
        with torch.cuda.amp.autocast():
            predictions = model(images, targets)
            losses = sum(loss for loss in predictions.values())

        # Adjusting the weights in a very clever way.
        optimizer.zero_grad()
        losses.backward()
        optimizer.step()

def evaluate(model, dataloader, device, epoch):
    model.eval()

    # Avoid type definition on purpose, as we aim to make an dataset
    # agnostic evaluator.
    evaluator = pfde.PennFudanDatasetEvaluator(
        pred_count  = 0,
        root_folder = f"result/pennfudan-experiment-011/epoch{epoch}",
        epoch       = epoch
    )

    for batch_images, batch_targets in dataloader:
        # Load entire batch of images to the SELECTED device.
        images  = [image.to(device) for image in batch_images]

        with torch.no_grad():
            predictions = model(images)

        evaluator(images=images, preds=predictions, gts=batch_targets)

def main():
    # The architecture of FRRCNN demands that the image is 
    # represented using float-type of variables rather than 
    # common integers, instead of 0-255, it wants 0.00-1.00.
    image_transform = torchvision.transforms.Compose([
        torchvision.transforms.PILToTensor()                 ,
        torchvision.transforms.ConvertImageDtype(torch.float)
    ])

    # Prepare a dataset.
    dataset = PennFudanDataset(
        root_path          = "/srv/sandbox/hardcode.heresy/python/pytorch-finetuning/data/PennFudanPed",
        annotations_folder = "Annotation"                                                              ,
        images_folder      = "PNGImages"                                                               ,
        image_transform    = image_transform                                                           ,
        target_transform   = None
    )

    # Split the dataset into the training and validation subsets.
    indices : list = torch.randperm(len(dataset)).tolist()
    subset_train   = torch.utils.data.Subset(dataset, indices[:-50])
    subset_val     = torch.utils.data.Subset(dataset, indices[-50:])

    # Create DataLoaders.
    dataloader_train = torch.utils.data.DataLoader(
        subset_train       ,
        batch_size   = 4   ,
        shuffle      = True,
        num_workers  = 4   ,
        collate_fn   = lambda batch : tuple(zip(*batch))
    )
    dataloader_val   = torch.utils.data.DataLoader(
        subset_val        ,
        batch_size  = 1   ,
        shuffle     = False,
        num_workers = 4   ,
        collate_fn   = lambda batch : tuple(zip(*batch))
    )

    # Train on the GPU or on the CPU, if a GPU is not available
    device = torch.device('cuda') if torch.cuda.is_available() else torch.device('cpu')

    # Create a model.
    model = architecture(num_classes=2)
    #odel = torch.load("model.pth")

    # Load the model to the selected device.
    model.to(device)

    # Essentially, a list of parameters inside the model which were
    # not frozen, therefore could be updated during the training.
    params = [p for p in model.parameters() if p.requires_grad]

    # Once the parameters, which are required to be "optimised", are
    # collected, we can create an optimiser.
    optimizer = torch.optim.SGD(
        params       = params,
        lr           = 0.005 ,
        momentum     = 0.9   ,
        weight_decay = 0.0005
    )

    # Consequently, we then create learning rate scheduler.
    lr_scheduler = torch.optim.lr_scheduler.StepLR(
        optimizer = optimizer,
        step_size = 3        ,
        gamma     = 0.1
    )

    num_epochs : int = 1000

    for epoch in range(num_epochs):
        # Optimise parameters on a training dataset, i.e. train.
        train(model, optimizer, dataloader_train, device)

        # Update learning rate.
        lr_scheduler.step()

        # Evaluate on a validation dataset.
        evaluate(model, dataloader_val, device, epoch)

        print(f"Finished epoch: {epoch}")

    torch.save(model, "model.pth")
main()
