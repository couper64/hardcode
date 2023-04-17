import os                as os
import pathlib           as pathlib
import tensorflow        as tf
import PIL.Image         as pil
import pandas            as pd
import numpy             as np
import tqdm              as tqdm

# Global constants. (Vocabulary)
IMAGE_ID   : str = "IMAGE_ID"
IMAGE_PATH : str = "IMAGE_PATH"
CLASS      : str = "CLASS"
SUPERCLASS : str = "SUPERClASS"

# Global constants. (Vocabulary)
MNIST  : str = "mnist"
TRAIN  : str = "train"
TEST   : str = "test"
IMAGES : str = "images"
GT     : str = "labels.csv"

def generate_dataset(data : np.ndarray, gt : np.ndarray, path : str):

    true_dict : dict = \
    {
        IMAGE_ID   : [],
        IMAGE_PATH : [],
        CLASS      : [],
        SUPERCLASS : []
    }

    for i, (x, y) in enumerate(tqdm.tqdm(zip(data, gt), total=len(data))):

        true_dict[IMAGE_ID  ].append(f"{i:0>5d}"                            )
        true_dict[IMAGE_PATH].append(os.path.join(IMAGES, f"{i:0>5d}.png"))
        true_dict[CLASS     ].append(str(y)                                 )
        true_dict[SUPERCLASS].append("digit"                                )

        image = pil.fromarray(obj=x, mode="L")
        image.save(fp=os.path.join(path, IMAGES, f"{i:0>5d}.png"))

    true_df : pd.DataFrame = pd.DataFrame(true_dict)
    true_df.to_csv(os.path.join(path, GT), index=False)

# Storing directory.
mnist_train_path : str = os.path.join(MNIST, TRAIN)
mnist_test_path  : str = os.path.join(MNIST, TEST )

# Create storing directory.
pathlib.Path(os.path.join(mnist_train_path, IMAGES))\
    .mkdir(parents=True, exist_ok=True)
pathlib.Path(os.path.join(mnist_test_path , IMAGES))\
    .mkdir(parents=True, exist_ok=True)

# An "x" denotes data, and "y" denotes labels.
(x_train, y_train), (x_test, y_test) = \
(
    tf.keras.datasets.mnist.load_data() # Stores under ~/.keras/datasets/
)

# From NumPy arrays to PNG files with custom annotation.
generate_dataset(x_train, y_train, mnist_train_path)
generate_dataset(x_test , y_test , mnist_test_path )