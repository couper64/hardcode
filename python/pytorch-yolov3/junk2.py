import cv2
from pytorchyolo import detect, models

# Load the YOLO model
model = models.load_model(
  "<PATH_TO_YOUR_CONFIG_FOLDER>/yolov3.cfg",
  "<PATH_TO_YOUR_WEIGHTS_FOLDER>/yolov3.weights")

# Load the image as a numpy array
img = cv2.imread("<PATH_TO_YOUR_IMAGE>")

# Convert OpenCV bgr to rgb
img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

# Runs the YOLO model on the image
boxes = detect.detect_image(model, img)

print(boxes)
# Output will be a numpy array in the following format:
# [[x1, y1, x2, y2, confidence, class]]