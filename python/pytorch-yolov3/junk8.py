import torch as torch
import itertools as it

class YOLO(torch.nn.Module):
    """Detection layer"""

    def __init__(self, anchors: list, num_classes: int):
        """
        Create a YOLO layer

        :param anchors: List of anchors
        :param num_classes: Number of classes
        """
        super(YOLO, self).__init__()
        self.num_anchors = len(anchors)
        self.num_classes = num_classes

        self.mse_loss = torch.nn.MSELoss()
        self.bce_loss = torch.nn.BCELoss()

        self.no = num_classes + 5  # number of outputs per anchor
        self.grid = torch.zeros(1)  # TODO

        anchors = torch.tensor(list(it.chain(*anchors))).float().view(-1, 2)
        self.register_buffer('anchors', anchors)
        self.register_buffer('anchor_grid', anchors.clone().view(1, -1, 1, 1, 2))
        self.stride = None

    def forward(self, x: torch.Tensor, image_size: int):
        """
        Forward pass of the YOLO layer

        :param x: Input tensor
        :param img_size: Size of the input image
        """
        stride = image_size // x.size(2)
        self.stride = stride
        bs, _, ny, nx = x.shape  # x(bs,255,20,20) to x(bs,3,20,20,85)
        x = x.view(bs, self.num_anchors, self.no, ny, nx).permute(0, 1, 3, 4, 2).contiguous()

        if not self.training:  # inference
            if self.grid.shape[2:4] != x.shape[2:4]:
                self.grid = self._make_grid(nx, ny).to(x.device)


            x[..., 0:2] = (x[..., 0:2].sigmoid() + self.grid) * stride  # xy
            x[..., 2:4] = torch.exp(x[..., 2:4]) * self.anchor_grid # wh
            x[..., 4:] = x[..., 4:].sigmoid() # conf, cls

            x = x.view(bs, -1, self.no)

        return x

    @staticmethod
    def _make_grid(nx: int = 20, ny: int = 20) -> torch.Tensor:
        """
        Create a grid of (x, y) coordinates

        :param nx: Number of x coordinates
        :param ny: Number of y coordinates
        """
        yv, xv = torch.meshgrid([torch.arange(ny), torch.arange(nx)], indexing='ij')
        return torch.stack((xv, yv), 2).view((1, 1, ny, nx, 2)).float()


class Model(torch.nn.Module):

    def __init__(self):
        super(Model, self).__init__()

        self.block_0 = torch.nn.Sequential\
        (
            torch.nn.Conv2d(in_channels=3, out_channels=255, kernel_size=1, stride=1, padding=0),
            torch.nn.BatchNorm2d(num_features=255, eps=1e-5, momentum=0.1),
            torch.nn.LeakyReLU(negative_slope=0.1)
        )

        self.yolo_layers = [YOLO(anchors=[(81,82), (135,169), (344,319)], num_classes=80)]

    def forward(self, x):
        return self.yolo_layers[0](self.block_0(x), image_size=600)

model : Model = Model()

optimizer = torch.optim.Adam\
(
    params = [p for p in model.parameters() if p.requires_grad],
    lr=1e-4,
    weight_decay=5e-4,
)

model.train()

# The following are an image id, class id, x, y, width, height.
targets = torch.Tensor([[0, 16, 0.400, 0.500, 0.500, 0.500],
                        [1, 15, 0.300, 0.400, 0.500, 0.400],
                        [2, 14, 0.200, 0.300, 0.400, 0.300],
                        [3, 13, 0.100, 0.200, 0.300, 0.200]])

# Input image size was 800x600.
pred = model(torch.rand([1, 3, 20, 20]))






loss_classification : torch.Tensor = torch.zeros(0)
loss_objectness     : torch.Tensor = torch.zeros(0)
loss_bounding_box   : torch.Tensor = torch.zeros(0)








# Here are the output of the conversion.
target_class        : list = []
target_bounding_box : list = []
indices             : list = [] # Not sure yet, what it really means.
target_anchors      : list = []








# The targets are provided in the user-readable format, but we want to convert 
# it to a format suitable to compare it with the results of the YOLO layer.
num_anchors : int = 3
num_targets : int = 4 # Because we prepared only a single target, i.e. labels.










# At this point in time, this is just an empty placeholder.
gain = torch.ones(7)










# A simple operation to create an array of values from 0 to "num_anchors",
# i.e. [0, 1, 2].
# But, we want it to be floats instead of integers.
anchors : torch.Tensor = torch.arange(num_anchors, dtype=torch.float)

# Have a 1D array of elements is inconvenient, we want to convert it to a 2D
# array, such so each element of the 1D array would become a row in the 2D 
# array, i.e. [0, 1, 2] => [[0], [1], [2]]
anchors = anchors.view(num_anchors, 1)

# I don't know yet, but for some reason, we want to repeat each row-element in 
# the 2D array as many times as we have targets (labels).
# tensor([[0.],                tensor([[0., 0., 0., 0., 0., 0.],
#         [1.],  should become         [1., 1., 1., 1., 1., 1.],
#         [2.]])                       [2., 2., 2., 2., 2., 2.]])
# where the "num_targets" value was 6 and the "num_anchors" value is 3.
anchors = anchors.repeat(1, num_targets)

# We want to add one more dimension, aka tensor = tensor[:, :, None].
# https://stackoverflow.com/questions/
# 69797614/indexing-a-tensor-with-none-in-pytorch
anchors = anchors.unsqueeze(dim=2)











# No idea, why we do this.
# Copy targets anchor-size-times and append an anchor index to each copy 
# of the anchor index is also expressed by the new first dimension
targets = targets.repeat(num_anchors, 1, 1)
targets = torch.cat([targets, anchors], 2)



# We are trying to convert the anchors from something like this 
# [(10,14), (23,27)] to get something like this tensor([[10., 14.],
#                                                       [23., 27.]])
anchors : torch.Tensor = torch.tensor\
(
    data  = [list(anchor) for anchor in [(81,82), (135,169), (344,319)]],
    dtype = torch.float
)

# A ratio between the original height of an image over the convoluted height 
# of the image. (BxCxHxW), e.g. 1x3x600x800 over 1x255x20x20.
ratio = 600/20

# I think, here, we are trying to convert coordinates of the anchors from 
# 800x600 to 20x20 coordinate space.
anchors = anchors / ratio
















# We want to store X and Y information of the prediction in the "gain" tensor.
# For reasons unknown to me.
# The "gain" tensor matches the collumns of our targets 
# (image id, class id, x, y, w, h, anchor id)
gain[2:6] = torch.tensor(pred[0].shape)[[2, 1, 2, 1]]  # xyxy gain

targets = targets * gain








# This is the magic part where we decide which anchors are suitable and 
# which anchors are unsuitable.
# Calculate ration between anchor and target box for both width and height
ratios = targets[:, :, 4:6] / anchors.unsqueeze(dim=1)

# Select the ratios that have the highest divergence in any axis 
# and check if the ratio is less than 4
divergence = torch.max(ratios, 1. / ratios).max(2)[0] < 4  # compare #TODO

# Only use targets that have the correct ratios for their anchors
# That means we only keep ones that have a matching anchor 
# and we lose the anchor dimension
# The anchor id is still saved in the 7th value of each target
targets = targets[divergence]

# Extract image id in batch and class id
image_id, class_id = targets[:, :2].long().T

# We isolate the target cell associations.
# x, y, w, h are allready in the cell coordinate system meaning
# an x = 1.2 would be 1.2 times cellwidth
grid_xy = targets[:, 2:4]
grid_wh = targets[:, 4:6]

# Cast to int to get a cell index e.g. 1.2 associated to cell 1.
grid_ij = grid_xy.long()

# Split x and y.
grid_i, grid_j = grid_ij.T  # Grid XY indices.

# Convert anchor indices to int.
anchor_indices = targets[:, 6].long()

# Add target tensors for this yolo layer to the output lists
# Add to index list and limit index range to prevent out of bounds
indices.append((image_id, anchor_indices, grid_j.clamp_(0, gain[3].long() - 1), grid_i.clamp_(0, gain[2].long() - 1)))
# Add to target box list and convert box coordinates from global grid coordinates to local offsets in the grid cell
target_bounding_box.append(torch.cat((grid_xy - grid_ij, grid_wh), 1))
# Add correct anchor for each target to the list
target_anchors.append(anchors[anchor_indices])
# Add class for each target to the list
target_class.append(class_id)

# Define different loss functions.
BCEcls = torch.nn.BCEWithLogitsLoss(pos_weight=torch.tensor([1.0]))
BCEobj = torch.nn.BCEWithLogitsLoss(pos_weight=torch.tensor([1.0]))



















import math

def bbox_iou(box1, box2, x1y1x2y2=True, GIoU=False, DIoU=False, CIoU=False, eps=1e-9):
    # Returns the IoU of box1 to box2. box1 is 4, box2 is nx4
    box2 = box2.T

    # Get the coordinates of bounding boxes
    if x1y1x2y2:  # x1, y1, x2, y2 = box1
        b1_x1, b1_y1, b1_x2, b1_y2 = box1[0], box1[1], box1[2], box1[3]
        b2_x1, b2_y1, b2_x2, b2_y2 = box2[0], box2[1], box2[2], box2[3]
    else:  # transform from xywh to xyxy
        b1_x1, b1_x2 = box1[0] - box1[2] / 2, box1[0] + box1[2] / 2
        b1_y1, b1_y2 = box1[1] - box1[3] / 2, box1[1] + box1[3] / 2
        b2_x1, b2_x2 = box2[0] - box2[2] / 2, box2[0] + box2[2] / 2
        b2_y1, b2_y2 = box2[1] - box2[3] / 2, box2[1] + box2[3] / 2

    # Intersection area
    inter = (torch.min(b1_x2, b2_x2) - torch.max(b1_x1, b2_x1)).clamp(0) * \
            (torch.min(b1_y2, b2_y2) - torch.max(b1_y1, b2_y1)).clamp(0)

    # Union Area
    w1, h1 = b1_x2 - b1_x1, b1_y2 - b1_y1 + eps
    w2, h2 = b2_x2 - b2_x1, b2_y2 - b2_y1 + eps
    union = w1 * h1 + w2 * h2 - inter + eps

    iou = inter / union
    if GIoU or DIoU or CIoU:
        # convex (smallest enclosing box) width
        cw = torch.max(b1_x2, b2_x2) - torch.min(b1_x1, b2_x1)
        ch = torch.max(b1_y2, b2_y2) - torch.min(b1_y1, b2_y1)  # convex height
        if CIoU or DIoU:  # Distance or Complete IoU https://arxiv.org/abs/1911.08287v1
            c2 = cw ** 2 + ch ** 2 + eps  # convex diagonal squared
            rho2 = ((b2_x1 + b2_x2 - b1_x1 - b1_x2) ** 2 +
                    (b2_y1 + b2_y2 - b1_y1 - b1_y2) ** 2) / 4  # center distance squared
            if DIoU:
                return iou - rho2 / c2  # DIoU
            elif CIoU:  # https://github.com/Zzh-tju/DIoU-SSD-pytorch/blob/master/utils/box/box_utils.py#L47
                v = (4 / math.pi ** 2) * \
                    torch.pow(torch.atan(w2 / h2) - torch.atan(w1 / h1), 2)
                with torch.no_grad():
                    alpha = v / ((1 + eps) - iou + v)
                return iou - (rho2 / c2 + v * alpha)  # CIoU
        else:  # GIoU https://arxiv.org/pdf/1902.09630.pdf
            c_area = cw * ch + eps  # convex area
            return iou - (c_area - union) / c_area  # GIoU
    else:
        return iou  # IoU













############# This chunk of code must be performed per YOLO layer which
############# should be define in the model.


## Just a name prefix to avoid name collision.
## loss_

# Calculate losses for each yolo layer
for layer_index, layer_predictions in enumerate(pred):

    # Get image ids, anchors, grid index i and j for each target in the current yolo layer
    loss_class_id, loss_anchor, loss_grid_j, loss_grid_i = indices[layer_index]

    # Build empty object target tensor with the same shape as the object prediction
    loss_target_object = torch.zeros_like(layer_predictions[..., 0])

    # Get the number of targets for this layer.
    # Each target is a label box with some scaling and the association of an anchor box.
    # Label boxes may be associated to 0 or multiple anchors. So they are multiple times or not at all in the targets.
    num_targets = loss_class_id.shape[0]

    # Check if there are targets for this batch
    if num_targets:
        # Load the corresponding values from the predictions for each of the targets
        layer_predictions = layer_predictions[loss_anchor, grid_j, grid_i]

        # Regression of the box
        # Apply sigmoid to xy offset predictions in each cell that has a target
        pxy = layer_predictions[:, :2].sigmoid()

        # Apply exponent to wh predictions and multiply with the anchor box that matched best with the label for each cell that has a target
        pwh = torch.exp(layer_predictions[:, 2:4]) * anchors[layer_index]
        # Build box out of xy and wh
        pbox = torch.cat((pxy, pwh), 1)
        # Calculate CIoU or GIoU for each target with the predicted box for its cell + anchor
        iou = bbox_iou(pbox.T, target_bounding_box[layer_index], x1y1x2y2=False, CIoU=True)
        # We want to minimize our loss so we and the best possible IoU is 1 so we take 1 - IoU and reduce it with a mean
        loss_bounding_box += (1.0 - iou).mean()  # iou loss

        # Classification of the objectness
        # Fill our empty object target tensor with the IoU we just calculated for each target at the targets position
        loss_target_object[loss_anchor, grid_j, grid_i] = iou.detach().clamp(0).type(loss_target_object.dtype)  # Use cells with iou > 0 as object targets

        # Classification of the class
        # Check if we need to do a classification (number of classes > 1)
        if layer_predictions.size(1) - 5 > 1:
            # Hot one class encoding
            t = torch.zeros_like(layer_predictions[:, 5:])  # targets
            t[range(num_targets), target_class[layer_index]] = 1
            # Use the tensor to calculate the BCE loss
            loss_classification += BCEcls(layer_predictions[:, 5:], t)  # BCE

    # Classification of the objectness the sequel
    # Calculate the BCE loss between the on the fly generated target and the network prediction
    #loss_objectness += BCEobj(layer_predictions[..., 4], loss_target_object) # obj loss























""