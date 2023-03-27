import pandas    as pd
import toolkit.data.format.synthetic_dataset_format as sdf

annotations100 : pd.DataFrame = pd.read_csv("annotations100.csv")
for column in sdf.GT_COLUMNS:
    if column in annotations100.columns:
        print(column)

results : pd.DataFrame = pd.read_csv("results.csv")
for column in sdf.PRED_COLUMNS:
    if column in results.columns:
        print(column)