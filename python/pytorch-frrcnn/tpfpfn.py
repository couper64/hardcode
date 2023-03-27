import pandas as pd

import toolkit.data.format.pennfudan_dataset_format as pfdf

def area(df : pd.DataFrame, columns : list):
    x0, y0, x1, y1 = [df[column] for column in columns]
    return (x1 - x0) * (y1 - y0)

def produce_tpfp(
        self                               ,
        gt_df                : pd.DataFrame,
        pred_df              : pd.DataFrame,
        confidence_threshold : float       ,
        iou_threshold        : float
    ):
    true_positive  : int = 0
    false_positive : int = 0

    fp : int = len(pred_df[pred_df[pfdf.SCORE] < confidence_threshold].index)

    # 1. Filter out predictions below the confidence threshold because
    #    they are FALSE POSTIVES.
    pred_df = pred_df.drop(pred_df[pred_df[pfdf.SCORE] < confidence_threshold].index)

    # Make a table of overlapping (by class) GT again Predictions.
    total_df : pd.DataFrame = gt_df.merge(pred_df, on=[pfdf.IMAGE_ID, pfdf.CLASS, pfdf.SUPERCLASS, pfdf.BBOX_ALIGNED], suffixes=("_GT", "_PRED"))

    # IoU min/max coordinates of intersection area.
    total_df["X_TOPLEFT"] = total_df[[f"{pfdf.BBOX_ALIGNED_X0}_GT", f"{pfdf.BBOX_ALIGNED_X0}_PRED"]].max(axis=1)
    total_df["Y_TOPLEFT"] = total_df[[f"{pfdf.BBOX_ALIGNED_Y0}_GT", f"{pfdf.BBOX_ALIGNED_Y0}_PRED"]].max(axis=1)
    total_df["X_BOTTOMRIGHT"] = total_df[[f"{pfdf.BBOX_ALIGNED_X1}_GT", f"{pfdf.BBOX_ALIGNED_X1}_PRED"]].min(axis=1)
    total_df["Y_BOTTOMRIGHT"] = total_df[[f"{pfdf.BBOX_ALIGNED_Y1}_GT", f"{pfdf.BBOX_ALIGNED_Y1}_PRED"]].min(axis=1)

    gt_intersect = (total_df["X_BOTTOMRIGHT"] > total_df["X_TOPLEFT"]) & \
                   (total_df["Y_BOTTOMRIGHT"] > total_df["Y_TOPLEFT"])

    total_df = total_df[gt_intersect]

    gt_areas   = area(total_df, [f"{pfdf.BBOX_ALIGNED_X0}_GT", f"{pfdf.BBOX_ALIGNED_Y0}_GT", f"{pfdf.BBOX_ALIGNED_X1}_GT", f"{pfdf.BBOX_ALIGNED_Y1}_GT"])
    pred_areas = area(total_df, [f"{pfdf.BBOX_ALIGNED_X0}_PRED", f"{pfdf.BBOX_ALIGNED_Y0}_PRED", f"{pfdf.BBOX_ALIGNED_X1}_PRED", f"{pfdf.BBOX_ALIGNED_Y1}_PRED"])
    intersect_areas = area(total_df, ["X_TOPLEFT", "Y_TOPLEFT", "X_BOTTOMRIGHT", "Y_BOTTOMRIGHT"])

    iou_areas = intersect_areas / (gt_areas + pred_areas - intersect_areas)

    total_df["IOU"] = iou_areas

    fp += len(total_df[total_df["IOU"] < iou_threshold].index)

    total_df = total_df.drop(total_df[total_df["IOU"] < iou_threshold].index)
    total_df = total_df.drop_duplicates([f"{pfdf.BBOX_ALIGNED_X0}_GT", f"{pfdf.BBOX_ALIGNED_Y0}_GT", f"{pfdf.BBOX_ALIGNED_X1}_GT", f"{pfdf.BBOX_ALIGNED_Y1}_GT"], keep="first")

    true_positive = len(total_df.index)
    false_positive = fp

    return (true_positive, false_positive)
true_df : pd.DataFrame = pd.read_csv("result/pennfudan-experiment-011/epoch999/groundtruth.csv")
pred_df : pd.DataFrame = pd.read_csv("result/pennfudan-experiment-011/epoch999/predictions.csv")

baseline_lhs : float = len(pred_df[pred_df[pfdf.SCORE] < 0.50].index)

precisions : list = []
recalls    : list = []

true_positives : int = 0

for confidence_threshold in range(0, 100, 1):

    tp, fp = produce_tpfp(
        None                       ,
        true_df                    ,
        pred_df                    ,
        confidence_threshold = float(confidence_threshold) / 100.00,
        iou_threshold        = 0.50
    )

    true_positives = tp

    #print(f"TP: {tp}, FP: {fp}, TP+FN: {len(true_df.index)}")

    precision : float = tp / (tp + fp)
    recall    : float = tp / len(true_df.index)

    precisions.append(precision)
    recalls   .append(recall)
    print(f"PRECISION: {precision:.2f}, RECALL: {recall:.2f}")

import matplotlib.pyplot as plt

fig, ax = plt.subplots(figsize=(6,6))
ax.plot(recalls, precisions, label='Logistic Regression')
#ax.plot(l2_recall_scores, l2_precision_scores, label='L2 Logistic Regression')
baseline = baseline_lhs / len(true_df.index)
ax.plot([0, 1], [baseline, baseline], linestyle='--', label='Baseline')
ax.set_xlabel('Recall')
ax.set_ylabel('Precision')
ax.legend(loc='center left')
plt.show()