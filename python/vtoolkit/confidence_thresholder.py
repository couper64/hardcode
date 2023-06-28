import pandas as pd

def discard_preds_less_than\
(
    threshold : float,
    from_df   : pd.DataFrame,
    column    : str
):
    return from_df.drop\
    (
        from_df[from_df[column] < threshold].index
    )

# true - ground-truth
# pred - prediction
true_df : pd.DataFrame = pd.read_csv("true.csv")
pred_df : pd.DataFrame = pd.read_csv("pred.csv")

# An intermediate container which would hold the
# processed result for evaluation.
df : pd.DataFrame = pred_df

# 1. The first step in the evaluation of AI models
#    is discarding predictions below certain
#    confidence threshold value, i.e. False Positives.
df = discard_preds_less_than(0.50, df, "SCORE")

# 2. The second step in the evaluation of AI models
#    is discarding predictions below certain
#    IoU threshold value, i.e. False Positives.
df = discard_preds_less_than(0.50, df, "IOU")