from pycocotools.coco     import COCO
from pycocotools.cocoeval import COCOeval


coco_gt = COCO("coco_annotation.json")
coco_dt = coco_gt.loadRes("coco_result.json")

coco_eval = COCOeval(coco_gt, coco_dt, iouType="bbox")
coco_eval.evaluate()
coco_eval.accumulate()
coco_eval.summarize()
