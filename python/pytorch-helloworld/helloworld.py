from random import shuffle
import torch       as torch
import torchvision as torchvision

from PIL           import Image

# Expecting:
# torch.nn
# torch.utils.data.DataLoader
# torchvision.datasets
# torchvision.transforms.ToTensor

# Torch Vision contains all the "vision" datasets, i.e. images.
# Here, we are trying to get a training split of dataset.
train_data = torchvision.datasets.FashionMNIST(
    root      = "data"                           , # A folder path, inside of which the downloaded datasets will be stored.
    train     = True                             ,
    download  = True                             ,
    transform = torchvision.transforms.ToTensor(),
)

# And, here a testing split. Why no validation?
# FashionMNIST contains Tuple[Any, Any], where in our case,
# Tuple[Tensor, ClassIndex]
test_data = torchvision.datasets.FashionMNIST(
    root      = "data"                           , # A folder path, inside of which the downloaded datasets will be stored.
    train     = False                            ,
    download  = True                             ,
    transform = torchvision.transforms.ToTensor(),
)


# Here, we pass Dataset -> DataLoader.
train_dataloader : torch.utils.data.DataLoader = torch.utils.data.DataLoader(train_data, batch_size=64, num_workers=4, shuffle=True)
test_dataloader  : torch.utils.data.DataLoader = torch.utils.data.DataLoader(test_data , batch_size=64, num_workers=4, shuffle=True)

# A little preview of the data.
# https://stackoverflow.com/questions/37689423/convert-between-nhwc-and-nchw-in-tensorflow
print(f"N - number of images in a batch")
print(f"C - number of channels of the image (ex: 3 for RGB, 1 for grayscale...)")
print(f"H - height of the image")
print(f"W - width of the image")
for X, y in test_dataloader:
    print(f"Shape of X [N, C, H, W]: {X.shape}")
    print(f"Shape of y: {y.shape} {y.dtype}")
    break


# Figuring out where we CAN run the model.
device : str = "cuda" if torch.cuda.is_available() else "mps" if torch.backends.mps.is_available() else "cpu"
print(f"Using {device} device")


# Creating the model.
class NeuralNetwork(torch.nn.Module):

    # For successfull, operation, this function is a MUST.
    def __init__(self):

        super().__init__()
        self.flatten = torch.nn.Flatten()
        self.linear_relu_stack = torch.nn.Sequential(
            torch.nn.Linear(28 * 28, 512),
            torch.nn.ReLU()              ,
            torch.nn.Linear(512, 512)    ,
            torch.nn.ReLU()              ,
            torch.nn.Linear(512, 10)     ,
        )

    # For successfull, operation, this function is a MUST.
    def forward(self, x):

        x      = self.flatten(x)
        logits = self.linear_relu_stack(x)
        return logits

# Execution.
model : NeuralNetwork = NeuralNetwork().to(device)
print(model)


# Next majour step, optimisation.
loss_fn   = torch.nn.CrossEntropyLoss()
optimiser = torch.optim.SGD(model.parameters(), lr=1e-3)


# The training. (In a single training loop, 
# the model makes predictions on the training dataset
# (fed to it in batches), and backpropagates 
# the prediction error to adjust the model’s parameters.)
def train(dataloader : torch.utils.data.DataLoader, model : NeuralNetwork, loss_fn, optimiser):

    model.train() # Set boolean to train = True. No actual behavior, only change of state.

    for batch, (X, y) in enumerate(dataloader):

        X, y = X.to(device), y.to(device) # Send data to the device?

        # Compute prediction error.
        pred = model(X)
        loss = loss_fn(pred, y)

        # Backpropagation.
        optimiser.zero_grad()
        loss.backward()
        optimiser.step()

        if batch % 100 == 0:
            loss, current = loss.item(), batch * len(X)
            print(f"loss: {loss:>7f}  [{current:>5d}/{len(dataloader.dataset):>5d}]")


# The testing, but I assume that's "our" validation.
def test(dataloader : torch.utils.data.DataLoader, model : NeuralNetwork, loss_fn):

    model.eval() # Set model to evaluation state, i.e. train = False.

    test_loss, correct = 0, 0

    with torch.no_grad():

        for X, y in dataloader:

            X, y = X.to(device), y.to(device) # Send data to the device?

            pred = model(X)
            test_loss += loss_fn(pred, y).item()
            correct += (pred.argmax(1) == y).type(torch.float).sum().item()

    test_loss /= len(dataloader)
    correct /= len(dataloader.dataset)

    print(f"Test Error: \n Accuracy: {(100*correct):>0.1f}%, Avg loss: {test_loss:>8f} \n")


# Perform actual training.
epochs = 50
for t in range(epochs):
    print(f"Epoch {t+1}\n-------------------------------")
    train(train_dataloader, model, loss_fn, optimiser)
    test(test_dataloader, model, loss_fn)
print("Done!")


# Saving models.
torch.save(model.state_dict(), "model.pth")
print("Saved PyTroch Model State to model.pth")


# Loading models.
model = NeuralNetwork()
model.load_state_dict(torch.load("model.pth"))


# Performing demonstration.
classes = [
    "T-shirt/top",
    "Trouser"    ,
    "Pullover"   ,
    "Dress"      ,
    "Coat"       ,
    "Sandal"     ,
    "Shirt"      ,
    "Sneaker"    ,
    "Bag"        ,
    "Ankle boot" ,
]
model.eval()
x, y = test_data[1][0], test_data[1][1]
with torch.no_grad():
    pred = model(x)
    predicted, actual = classes[pred[0].argmax(0)], classes[y]
    print(f"Predicted: '{predicted}', Actual '{actual}'")