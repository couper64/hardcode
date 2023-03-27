# The CENTERNET model is a head in the grand scheme of the AI DNN.
# The backbone is DLA-34 which is dependant on DCNv2.
# DCNv2 is depedant on CUDA.
# CUDA is NVIDIA => PAIN!

# Anyways...

# DLA name is DLA.
BACKBONE_NAME : str = "DLA"
# DLA-34 has 34 layers.
NUM_LAYERS    : int = 34

# The "heads" later need a number of classes.
# This could be provided by the user, I am okay with this.
NUM_CLASSES : int = 16

# It requires a very complex dictionary named "heads".
# As for the 3D prediction task, we have the following 
# parameters.
HEADS : dict = \
{
    "hm" : NUM_CLASSES, # Unknown abbreviation.
    "dep": 1, # Unknown abbreviation and magic number.
    "rot": 8, # Unknown abbreviation and magic number. Maybe "rotation"?
    "dim": 3, # Unknown abbreviation and magic number. Maybe "dimension"?

    # Plus, optinally, it defines the following, in case, if we 
    # select "reg_bbox" and "reg_offset".
    "wh" : 2, # Unknown abbreviation and magic number.
    "reg": 2  # Unknown abbreviation and magic number.
}

# Next, it requires "head_conv". According to the documentation, it is
# "conv layer channels for output head
#   0 for no conv layer
#  -1 for default setting: 
#  64 for resnets and 256 for dla."
HEAD_CONV : int = 256 # As per documentation.

# Internally, DLA requires a "down_ratio" parameter which was set to 4.
DOWN_RATIO : int = 4

# By default, it is set to use pretrained model. Plus, the "final_kernel"
# set to 1, the "last_level" is set to 5. God knows what it means.
FINAL_KERNEL : int = 1
LAST_LEVEL   : int = 5

# Forgot to mention, from this point on, we need PyTorch.
import torch    as torch
import torch.nn as nn

# ... and, numpy.
import numpy    as np

# The DLA depends on BasicBlock, so, first, we have to define it.
class BasicBlock(nn.Module):

    def __init__\
    (
        self                  , # DLA is undefined at this point.
        in_channels  : int    , # No idea, why "inplanes"?
        out_channels : int    , # No idea, why "planes"?
        stride       : int = 1, # I have a rough idea, but
                                # better double check it.
        dilation     : int = 1  # No idea, why "dilation"?
    ):
        super(BasicBlock, self).__init__()

        # Finally, we see some architecture.
        self.conv1 = nn.Conv2d\
        (
            in_channels  = in_channels ,
            out_channels = out_channels,
            kernel_size  = 3           , # Hardcoded.
            stride       = stride      ,
            padding      = dilation    ,
            bias = False               , # Hardcoded.
            dilation     = dilation
        )
        self.bn1  = nn.BatchNorm2d(num_features = out_channels, momentum = 0.1)
        self.relu = nn.ReLU       (inplace = True)

        self.conv2 = nn.Conv2d\
        (
            in_channels  = out_channels,
            out_channels = out_channels,
            kernel_size  = 3           , # Hardcoded.
            stride       = 1           , # Hardcoded.
            padding      = dilation    ,
            bias         = False       , # Hardcoded.
            dilation     = dilation
        )
        self.bn2 = nn.BatchNorm2d(num_features = out_channels, momentum = 0.1)

    def forward(self, x : torch.Tensor):

        identity : torch.Tensor = x

        x = self.conv1(x)
        x = self.bn1  (x)
        x = self.relu (x)

        x = self.conv2(x)
        x = self.bn2  (x)

        x += identity

        x = self.relu (x)

        return x




# We need to define a DLA module.
class DLA(nn.Module):

    def __init__\
    (
        self              , # DLA is undefined at this point.
        levels      : list, # A list of integers.
        channels    : list, # A list of integers.
        num_classes : int
    ):
        super(DLA, self).__init__()

# Well, from the code, I could tell, that the first thing is DLASeg.
# The DLASeg takes in all the parameters defined above. Also,
# DLASeg is a nn.Module with a constructor and single forward pass function.
class DLASeg(nn.Module):

    def __init__\
    (
        self                , # DLASeg is undefined at this point.
        last_level : int    ,
        down_ratio : int = 2, # From what I understood, 
                              # this is an integer starting from 2.
    ):
        super(DLASeg, self).__init__() # Init our parent.

        # At this point, the original authors decided to check if the
        # down_ratio is within expected range.
        assert down_ratio in [2, 4, 8, 16] # No 32, why?

        self.first_level : int = int(np.log2(down_ratio)) # Is int(...)
                                                          # redundant?

        self.last_level  : int = last_level

        # The authors play a very confusing trick using globals() to 
        # access a global function. Anyhow, it cames down to a simple 
        # initialisation of DLA-34 architecture.


    def forward(self, x : torch.Tensor):
        z : dict = {}
        return [z]