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

# Column names of the non-axis-aligned bounding-box annotation.
BBOX_FREE    : str = "BBOX_FREE"
BBOX_FREE_X0 : str = "BBOX_FREE_X0"
BBOX_FREE_Y0 : str = "BBOX_FREE_Y0"
BBOX_FREE_X1 : str = "BBOX_FREE_X1"
BBOX_FREE_Y1 : str = "BBOX_FREE_Y1"
BBOX_FREE_X2 : str = "BBOX_FREE_X2"
BBOX_FREE_Y2 : str = "BBOX_FREE_Y2"
BBOX_FREE_X3 : str = "BBOX_FREE_X3"
BBOX_FREE_Y3 : str = "BBOX_FREE_Y3"

# Column names of the non-axis-aligned bounding-box annotation.
BBOX_FREE_COLUMNS : list = [
    BBOX_FREE, BBOX_FREE_X0, BBOX_FREE_Y0,
               BBOX_FREE_X1, BBOX_FREE_Y1,
               BBOX_FREE_X2, BBOX_FREE_Y2,
               BBOX_FREE_X3, BBOX_FREE_Y3,
]

# Column names of the 3D bounding-box annotation.
BBOX_3D                : str = "BBOX_3D"
BBOX_3D_LOC_X          : str = "BBOX_3D_LOC_X"
BBOX_3D_LOC_Y          : str = "BBOX_3D_LOC_Y"
BBOX_3D_LOC_Z          : str = "BBOX_3D_LOC_Z"
BBOX_3D_WIDTH          : str = "BBOX_3D_WIDTH"
BBOX_3D_HEIGHT         : str = "BBOX_3D_HEIGHT"
BBOX_3D_LENGTH         : str = "BBOX_3D_LENGTH"
BBOX_3D_PI_ROT_Y       : str = "BBOX_3D_PI_ROT_Y"
BBOX_3D_PMAT_COL0_ROW0 : str = "BBOX_3D_PMAT_COL0_ROW0"
BBOX_3D_PMAT_COL1_ROW0 : str = "BBOX_3D_PMAT_COL1_ROW0"
BBOX_3D_PMAT_COL2_ROW0 : str = "BBOX_3D_PMAT_COL2_ROW0"
BBOX_3D_PMAT_COL3_ROW0 : str = "BBOX_3D_PMAT_COL3_ROW0"
BBOX_3D_PMAT_COL0_ROW1 : str = "BBOX_3D_PMAT_COL0_ROW1"
BBOX_3D_PMAT_COL1_ROW1 : str = "BBOX_3D_PMAT_COL1_ROW1"
BBOX_3D_PMAT_COL2_ROW1 : str = "BBOX_3D_PMAT_COL2_ROW1"
BBOX_3D_PMAT_COL3_ROW1 : str = "BBOX_3D_PMAT_COL3_ROW1"
BBOX_3D_PMAT_COL0_ROW2 : str = "BBOX_3D_PMAT_COL0_ROW2"
BBOX_3D_PMAT_COL1_ROW2 : str = "BBOX_3D_PMAT_COL1_ROW2"
BBOX_3D_PMAT_COL2_ROW2 : str = "BBOX_3D_PMAT_COL2_ROW2"
BBOX_3D_PMAT_COL3_ROW2 : str = "BBOX_3D_PMAT_COL3_ROW2"

# 3D bounding-box project matrix.
BBOX_3D_PMAT_COLUMNS : list = [
    BBOX_3D_PMAT_COL0_ROW0, BBOX_3D_PMAT_COL1_ROW0, BBOX_3D_PMAT_COL2_ROW0, BBOX_3D_PMAT_COL3_ROW0,
    BBOX_3D_PMAT_COL0_ROW1, BBOX_3D_PMAT_COL1_ROW1, BBOX_3D_PMAT_COL2_ROW1, BBOX_3D_PMAT_COL3_ROW1,
    BBOX_3D_PMAT_COL0_ROW2, BBOX_3D_PMAT_COL1_ROW2, BBOX_3D_PMAT_COL2_ROW2, BBOX_3D_PMAT_COL3_ROW2,
]

# Column names of the 3D bounding-box annotation.
BBOX_3D_COLUMNS : list = [
    BBOX_3D, BBOX_3D_LOC_X, BBOX_3D_LOC_Y , BBOX_3D_LOC_Z ,
             BBOX_3D_WIDTH, BBOX_3D_HEIGHT, BBOX_3D_LENGTH,
    BBOX_3D_PI_ROT_Y,
] + BBOX_3D_PMAT_COLUMNS

# Columns names for grouth-truth and prediction annotations.
IMAGE_ID   : str = "IMAGE_ID"
IMAGE_PATH : str = "IMAGE_PATH"
SCORE      : str = "SCORE"
CLASS      : str = "CLASS"
SUPERCLASS : str = "SUPERCLASS"

# Columns names for grouth-truth annotations.
GT_COLUMNS : list = [
    IMAGE_ID, IMAGE_PATH, CLASS, SUPERCLASS,
] + BBOX_ALIGNED_COLUMNS + BBOX_FREE_COLUMNS + BBOX_3D_COLUMNS

# Columns names for prediction annotations.
PRED_COLUMNS : list = [
    IMAGE_ID, SCORE, CLASS, SUPERCLASS,
] + BBOX_ALIGNED_COLUMNS + BBOX_FREE_COLUMNS + BBOX_3D_COLUMNS
