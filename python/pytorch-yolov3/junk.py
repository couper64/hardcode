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

        self.conv_0 = torch.nn.Sequential\
        (
            torch.nn.Conv2d(in_channels=3, out_channels=255, kernel_size=1, stride=1, padding=0)
        )

    def forward(self, x):
        x = self.conv_0(x)
        return YOLO(anchors=[(81,82), (135,169), (344,319)], num_classes=80)(x, image_size=20)

model : Model = Model()

optimizer = torch.optim.Adam(
    params = [p for p in model.parameters() if p.requires_grad],
    lr=1e-4,
    weight_decay=5e-4,
)

model.train()

# image id, class id, x (pixels), y (pixels), width (pixels), height (pixels)
targets = (0, 1, 300, 200, 500, 400)

predictions : torch.Tensor = model(torch.rand([1, 3, 20, 20]))

# we need to compute loss.
target_class : list = [0] # class id, for example, 0 means person
target_bbox  : list = [(0.3, 0.2, 0.5, 0.4)]
target_indices : list = [(0, 0, 3, 2)] # image id, anchor id, cell x, cell y
target_anchors : list = [0] # an index which points to an anchor

# Define different loss functions classification
BCEcls = torch.nn.BCEWithLogitsLoss(
    pos_weight=torch.tensor([1.0], device="cpu"))
BCEobj = torch.nn.BCEWithLogitsLoss(
    pos_weight=torch.tensor([1.0], device="cpu"))
lbox = torch.zeros(1, device="cpu")

loss = lbox + BCEcls([1, 1, 1, 1, 1]) + BCEobj()

loss.backward()

print(predictions.shape)