The work is based on the PyTorch tutorial available at 
the [link](https://pytorch.org/tutorials/intermediate/torchvision_tutorial.html).
However, I made my own adjustments according to my needs which are related only to 
the object detection without the object segmentation.

I will be using __conda__ to create __pytorch-finetuning__ environment with
an assumption that I have Ubuntu 20.04.5 with 5.15.0-58-generic kernel 
with NVIDIA proprietary 515.86.01 driver and CUDA 11.7.0 runtime support
and CUDA 11.7.0 SDK. The commands are 

    clear; conda create -yn pytorch-finetuning python=3.9.13                    \
                            pytorch=1.13.1 torchvision=0.14.1 torchaudio=0.13.1 \
                            pytorch-cuda=11.7 cuda                              \
                            -c "nvidia/label/cuda-11.7.0" -c pytorch