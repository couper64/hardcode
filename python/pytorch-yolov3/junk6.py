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
        return self.yolo_layers[0](self.block_0(x), image_size=20)

model : Model = Model()

optimizer = torch.optim.Adam\
(
    params = [p for p in model.parameters() if p.requires_grad],
    lr=1e-4,
    weight_decay=5e-4,
)

model.train()

# The following are image id, class id, x, y, width, height.
targets = torch.Tensor([[0, 16, 0.4, 0.5, 0.6, 0.5]])

# Input image size was 640x480.
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

# The following are an image id, class id, x, y, width, height.
targets = torch.Tensor([[0, 16, 0.4, 0.5, 0.6, 0.5],
                       [1, 15, 0.3, 0.4, 0.5, 0.4],
                       [2, 14, 0.2, 0.3, 0.4, 0.3],
                       [3, 13, 0.1, 0.2, 0.3, 0.2]])

# No idea, why we do this.
# Copy targets anchor-size-times and append an anchor index to each copy 
# of the anchor index is also expressed by the new first dimension
targets = targets.repeat(num_anchors, 1, 1)
targets = torch.cat([targets, anchors], 2)

# This part should be within a loop.

# We are trying to convert the anchors from something like this 
# [(10,14), (23,27)] to get something like this tensor([[10., 14.],
#                                                       [23., 27.]])
anchors : torch.Tensor = torch.tensor\
(
    data  = [list(anchor) for anchor in [(10,14), (23,27)]],
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

# At this point we want to exit the loop.

# I still don't understand why do we do this multiplications yet, 
# but these were in the source code.
loss_classification *= 0.5
loss_objectness     *= 1.0
loss_bounding_box   *= 0.05

loss = loss_classification + loss_objectness + loss_bounding_box








# Example:

from pytorchyolo.utils.loss import compute_loss
loss, loss_components = compute_loss(pred, targets, model)

loss.backward()

# Run optimizer
optimizer.step()
# Reset gradients
optimizer.zero_grad()