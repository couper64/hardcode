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

def get_concat_h_resize(
    im1
    , im2
    , resample=Image.NEAREST
    , resize_big_image=False
):
    if im1.height == im2.height:
        _im1 = im1
        _im2 = im2
    elif (((im1.height > im2.height) and resize_big_image) or
          ((im1.height < im2.height) and not resize_big_image)):
        _im1 = im1.resize(
            (int(im1.width * im2.height / im1.height), im2.height)
            , resample=resample
        )
        _im2 = im2
    else:
        _im1 = im1
        _im2 = im2.resize(
            (int(im2.width * im1.height / im2.height), im1.height)
            , resample=resample
        )
    dst = Image.new('RGB', (_im1.width + _im2.width, _im1.height))
    dst.paste(_im1, (0, 0))
    dst.paste(_im2, (_im1.width, 0))
    return dst

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

def crop_left_image(image : Image.Image):

    left = 480
    top = 408
    right = 720
    bottom = 542

    return image.crop((left, top, right, bottom))

def crop_right_image(image : Image.Image):

    left = 2400
    top = 408
    right = 2640
    bottom = 542

    return image.crop((left, top, right, bottom))

folder_a_path : str = (
    "input_a"
)
folder_c_path : str = (
    "output"
)
images_a_names : list = get_filenames_from_folder(
    os.path.normpath(folder_a_path)
    , success_message="Success!"
    , failure_message="Failure!"
)
images_c_names : list = images_a_names


for i in tqdm(range(len(images_a_names))):
    image_a_name : str = images_a_names[i]
    image_c_name : str = images_c_names[i]
    image_a_path : str = os.path.join(folder_a_path, image_a_name)
    image_c_path : str = os.path.join(folder_c_path, image_c_name)
    image_a : Image.Image = Image.open(image_a_path)
    image_crop_a : Image.Image = crop_left_image(image_a)
    image_crop_b : Image.Image = crop_right_image(image_a)
    get_concat_h_resize(
        image_crop_a
        , image_crop_b
        , Image.NEAREST, False
    ).save(image_c_path)
