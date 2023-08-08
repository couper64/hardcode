import torch as torch

# 5 rows   , 4 columns
# 5 batches, 4 classes
target = torch.rand([5, 4], dtype=torch.float32)
print(target)

# This is a prediction represented as in a form of logits.
pred = torch.rand([5, 4])
print(pred)

# As far as I understood, this is a way to control to trade off recall and 
# precision.
weight = torch.ones([4])
print(weight)

criterion = torch.nn.BCEWithLogitsLoss(pos_weight=weight)(pred, target)
print(criterion)