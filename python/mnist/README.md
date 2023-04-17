clear ; conda create -yn mnist python=3.9.13 pandas tqdm \
tensorflow=2.10.0 cudatoolkit=11.7 cuda pillow \
-c conda-forge -c "nvidia/label/cuda-11.7.0"

The script will download the MNIST dataset into a local folder. The images will
be stored in PNG format and ground-truth annotations will be stored in CSV 
format.
