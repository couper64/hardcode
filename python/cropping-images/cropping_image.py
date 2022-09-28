# To resize the image. Link from Vasilis.
# https://stackoverflow.com/questions/39522881/
# shrink-resize-an-image-without-interpolation/55484414

# To enable virtual environment.
# ---> Set-ExecutionPolicy Unrestricted -Scope CurrentUser
# ---> ./.venv/Scripts/Activate.ps1
# ---> Set-ExecutionPolicy Restricted -Scope CurrentUser

# Use python -m pip to upgrade pip, not pip itself.
# https://stackoverflow.com/questions/31172719/
# pip-install-access-denied-on-windows
# Otherwise, I get Access denied errors on Windows.

# How to crop an image using Pillow.
# https://stackoverflow.com/questions/20361444/
# cropping-an-image-with-python-pillow

import os
from PIL import Image
from tqdm import tqdm

def get_filenames_from_folder(
    root: str = u""
    , success_message: str = u"File is Found!"
    , failure_message: str = u"No File is Found!"
):

    """
    Look up files in a folder.
    """

    # Local variables.
    filenames: list = []

    # Check for all non-folder files in the directory.
    for filename in os.listdir(root):
        if os.path.isfile(os.path.join(root, filename)):
            filenames.append(filename)

    # Finally, check if we got anything and report to developer.
    if len(filenames) > 0:
        print(success_message)
        return filenames

    else:
        print(failure_message)
        return None

def crop_image(image : Image.Image):

    left = 800
    top = 840
    right = 1760
    bottom = 1110

    return image.crop((left, top, right, bottom))

folder_input_path : str = "input"
folder_output_path : str = "output"

images_input_names : list = get_filenames_from_folder(
    os.path.normpath(folder_input_path)
    , success_message="Success!"
    , failure_message="Failure!"
)
images_output_names : list = images_input_names


for i in tqdm(range(len(images_input_names))):

    # Retrieve image names.
    image_input_name : str = images_input_names[i]
    image_output_name : str = images_output_names[i]

    # Generate image's full path for both input and output.
    image_input_path : str = os.path.join(
        folder_input_path
        , image_input_name
    )
    image_output_path : str = os.path.join(
        folder_output_path
        , image_output_name
    )

    # Crop image and save it.
    image_input : Image.Image = Image.open(image_input_path)
    crop_image(image_input).save(image_output_path)
