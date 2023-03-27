# Column names of the axis-aligned bounding-box annotation.
BBOX_ALIGNED    : str = "BBOX_ALIGNED"
BBOX_ALIGNED_X0 : str = "BBOX_ALIGNED_X0"
BBOX_ALIGNED_Y0 : str = "BBOX_ALIGNED_Y0"
BBOX_ALIGNED_X1 : str = "BBOX_ALIGNED_X1"
BBOX_ALIGNED_Y1 : str = "BBOX_ALIGNED_Y1"

# Column names of the axis-aligned bounding-box annotation.
BBOX_ALIGNED_COLUMNS : list = [
    BBOX_ALIGNED, BBOX_ALIGNED_X0, BBOX_ALIGNED_Y0,
                  BBOX_ALIGNED_X1, BBOX_ALIGNED_Y1,
]

# Columns names for grouth-truth and prediction annotations.
IMAGE_ID   : str = "IMAGE_ID"
IMAGE_PATH : str = "IMAGE_PATH"
SCORE      : str = "SCORE"
CLASS      : str = "CLASS"
SUPERCLASS : str = "SUPERCLASS"

# Columns names for grouth-truth annotations.
GT_COLUMNS : list = [
    IMAGE_ID, IMAGE_PATH, CLASS, SUPERCLASS,
] + BBOX_ALIGNED_COLUMNS

# Columns names for prediction annotations.
PRED_COLUMNS : list = [
    IMAGE_ID, SCORE, CLASS, SUPERCLASS,
] + BBOX_ALIGNED_COLUMNS