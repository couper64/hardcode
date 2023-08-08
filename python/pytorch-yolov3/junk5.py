import torch as torch

class Model(torch.nn.Module):

    def __init__(self):
        super(Model, self).__init__()

        self.block_0 : torch.nn.Sequential\
        (
            torch.nn.Conv2d     (in_channels=3, out_channels=16, kernel_size=3, stride=2, padding=0, bias=False),
            torch.nn.BatchNorm2d(num_features=16, eps=1e-5, momentum=0.1),
            torch.nn.LeakyReLU  (negative_slope=0.1)
        )

    def forward(self, x):
        return self.block_0(x)

model : Model = Model()
[print(p) for p in model.parameters() if p.requires_grad]
