import os          as os
import pathlib     as plib
import pandas      as pd
import torch       as torch
import torchvision as torchvision
import PIL.Image   as pilimage

import toolkit.data.format.pennfudan_dataset_format as pfdf

class PennFudanDatasetEvaluator(object):
    def __init__(
        self, pred_count : int, root_folder : str, epoch : int
    ):

        self.pred_df : pd.DataFrame = pd.DataFrame( # The Predictions CSV file.
            columns=pfdf.PRED_COLUMNS
        )
        self.gt_df   : pd.DataFrame = pd.DataFrame( # The Ground-Truth CSV file.
            columns=pfdf.GT_COLUMNS
        )

        self.class_map   : list = [
            "background", "pedestrian"
        ] # Integer to String.

        self.pred_count  : int = pred_count # Withing an epoch, then reset.
        self.root_folder : str = root_folder
        self.epoch       : int = epoch

        # A separate folder for images to keep it tidy.
        self.visualisation_folder : str = "visualisation"

        # File IO preparations.
        self.visualisation_path : str = os.path.join(
            root_folder, self.visualisation_folder
        )
        plib.Path(root_folder).mkdir(parents=True, exist_ok=True)
        plib.Path(self.visualisation_path).mkdir(parents=True, exist_ok=True)
    
    def __call__(self, *args, **kwds):

        # Handle kwds in the beginning to bring related errors together.
        preds  : list = kwds["preds" ]
        gts    : list = kwds["gts"   ]
        images : list = kwds["images"]

        # During inference, the model requires only the input tensors, and 
        # returns the post-processed predictions as a List[Dict[Tensor]], 
        # one for each input image. The fields of the Dict are as follows, 
        # where N is the number of detections:
        #   boxes (FloatTensor[N, 4]): the predicted boxes in [x1, y1, x2, y2] 
        # format, with 0 <= x1 < x2 <= W and 0 <= y1 < y2 <= H.
        #   labels (Int64Tensor[N]): the predicted labels for each detection
        #   scores (Tensor[N]): the scores of each detection
        for pred_index in range(len(preds)):

            # Because it is referenced so many times.
            pred : dict = preds[pred_index]
            gt   : dict = gts  [pred_index]

            # Because by len(pred["labels"]) we infer the number of detections
            # within a single prediction.
            detection_count : int  = len(pred["labels"])

            # Store the predictions and ground truths the results under a new
            # new because the images might have been mangled during the 
            # data augmentation stage.

            # Map label IDs to label names.
            gt_labels : list = [] # pred_index matches gts' index.
            for label_id in gt["labels"].tolist():
                label : str = self.class_map[label_id]
                gt_labels.append(label)

            image_path : str = os.path.join(
                self.visualisation_folder,
                f"image{self.pred_count}.png"
            )
            for index in range(len(gt_labels)):
                self.gt_df.loc[len(self.gt_df)] = [
                    f"image{self.pred_count}"   , # IMAGE_ID
                    image_path                  , # IMAGE_PATH
                    gt_labels[index]            , # CLASS
                    gt_labels[index]            , # SUPERCLASS
                    1                           , # BBOX_ALIGNED,
                                                  # 1 - means:
                                                  # "Yes, aligned bbox."
                    gt["boxes"][index][0].item(), # BBOX_ALIGNED_X0
                    gt["boxes"][index][1].item(), # BBOX_ALIGNED_Y0
                    gt["boxes"][index][2].item(), # BBOX_ALIGNED_X1
                    gt["boxes"][index][3].item()  # BBOX_ALIGNED_Y1
                ]

            for detection in range(detection_count):
                box   : list  = pred["boxes" ][detection]
                label : int   = pred["labels"][detection]
                score : float = pred["scores"][detection]

                self.pred_df.loc[len(self.pred_df)] = [
                    f"image{self.pred_count}"   , # IMAGE_ID
                    score.item()                , # SCORE
                    self.class_map[label.item()], # CLASS
                    self.class_map[label.item()], # SUPERCLASS
                    1            , # BBOX_ALIGNED,
                                   # 1 - means:
                                   # "Yes, aligned bbox."
                    box[0].item(), # BBOX_ALIGNED_X0
                    box[1].item(), # BBOX_ALIGNED_Y0
                    box[2].item(), # BBOX_ALIGNED_X1
                    box[3].item()  # BBOX_ALIGNED_Y1
                ]

            # Do the visualisatin before incrementing the pred_count, otherwise,
            # it will create a mismatch between the CSV and PNG files.
            self.visualise(
                images      = images          ,
                pred_index  = pred_index      ,
                pred_count  = self.pred_count ,
                preds       = preds           ,
                gts         = gts             ,
                class_map   = self.class_map  ,
            )

            self.pred_count += 1 # Increment to differentiate images.

        # Once all the predictions were gathered, save it to a CSV file.
        self.gt_df.to_csv(
            os.path.join(self.root_folder, "groundtruth.csv"), index=False
        )
        self.pred_df.to_csv(
            os.path.join(self.root_folder, "predictions.csv"), index=False
        )

    # Visualisation component rev4:
    def visualise(
        self              ,
        images      : list,
        pred_index  : int ,
        pred_count  : int , # To count the number of images in epoch.
        preds       : list,
        gts         : list,
        class_map   : list
    ):
        # Because it is referenced so many times.
        pred : dict = preds[pred_index]

        # Because by len(pred["labels"]) we infer the number of detections
        # within a single prediction.
        detection_count : int  = len(pred["labels"])

        # Prepare a list of the "label: score" statements to draw.
        pred_labels : list = []
        for detection in range(detection_count):
            label_id : int   = pred["labels"][detection].item()
            label    : str   = class_map[label_id] # ID matches index.
            score    : float = pred['scores'][detection]
            pred_labels.append(f"{label}: {score:.2f}")

        # Prepare a list of the "label: gt" statements to draw.
        # Number of pred_index matches the number of images 
        # what matches the number of ground-truths (gts).
        gt_labels : list = []
        for label_id in gts[pred_index]["labels"].tolist():
            label : str = class_map[label_id]
            gt_labels.append(f"{label}: gt")

        # The FRRCNN operates on torch.float type of variables, however,
        # PIL Image operates on unsigned integers in range 
        # between 0 and 255. Hence, we convert to torch.uint8.
        transform_to_uint8_fn = (
            torchvision.transforms.ConvertImageDtype(torch.uint8)
        ) # Extra brackets to fit the line under 80 characters.

        # Draw big ground-truth in the background.
        image_tensor = torchvision.utils.draw_bounding_boxes(
            image     = transform_to_uint8_fn(images[pred_index]),
            boxes     = gts[pred_index]["boxes"]                 ,
            labels    = gt_labels                                ,
            colors    = "lime"                                   ,
            width     = 3                                        ,
            font      = "UbuntuMono-R.ttf"                       ,
            font_size = 16 #pt, it won't work without font above.
        )

        # Draw smaller predictions in the foreground.
        image_tensor = torchvision.utils.draw_bounding_boxes(
            image     = image_tensor              ,
            boxes     = preds[pred_index]["boxes"],
            labels    = pred_labels               ,
            colors    = "magenta"                 ,
            font      = "UbuntuMono-R.ttf"        ,
            font_size = 12 #pt, it won't work without font above.
        )

        # Finally, save the image.
        transform_tensor_to_image_fn = torchvision.transforms.ToPILImage()
        image : pilimage.Image = transform_tensor_to_image_fn(image_tensor)
        image.save(f"{self.visualisation_path}/image{pred_count}.png")

    def produce_tpfpfn(
            self                               ,
            gt_df                : pd.DataFrame,
            pred_df              : pd.DataFrame,
            confidence_threshold : float       ,
            iou_threshold        : float
        ):
        true_positive  : int = 0
        false_positive : int = 0
        false_negative : int = 0

        # todo


        return (true_positive, false_positive, false_negative)
