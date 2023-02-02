# Main

The virtual environment is provided by __conda__ (miniforge-pypy3, base Python version is 3.9.12):

    clear; conda create -yn pytorch-helloworld python=3.9.13 pytorch torchvision torchaudio pytorch-cuda=11.7 cuda -c "nvidia/label/cuda-11.7.0" -c pytorch

As of right now, PyTorch is installed either via __conda__ or via __pip__. In my use case, right now, I am bound to CUDA 11.7 and conda-forge channel, as depicted above.

Here is a backlog for original commands:

    clear; conda create -yn pytorch-helloworld python=3.9.13
    clear; conda activate pytorch-helloworld
    clear; conda install -c "nvidia/label/cuda-11.7.0" cuda
    clear; conda install pytorch torchvision torchaudio pytorch-cuda=11.7 -c pytorch -c nvidia

Optionally, the __cuda__ command could be replaced with the following:

    clear; conda install -c "nvidia/label/cuda-11.7.0" cuda-toolkit

If the __cuda__ command is replaced, then it will be missing __cuda__, __cuda-demo-suite__, and __cuda-runtime__ packages.

PyTorch Tutorials:
* Working with data
  * [Intro](https://pytorch.org/tutorials/beginner/basics/intro.html) - a page with an overview.
  * [Quickstart](https://pytorch.org/tutorials/beginner/basics/quickstart_tutorial.html) - an overview of the API for common tasks.
  * Introduces to DataLoader and Dataset.
  * Introduces to TorchText, TorchVision, and TorchAudio which are domain specific datasets.
      * Every TorchVision Dataset includes two arguments: __transform__ and __target_transform__.
  * Dataset -> DataLoader, the idea is to pass Dataset to DataLoader.
* Create Models
  * We define the layers of the network in the __init__ function.
  * Specify how data will pass through the network in the forward function.
  * To accelerate operations in the neural network, we move it to the GPU if available.

```
# Get cpu or gpu device for training.
device = "cuda" if torch.cuda.is_available() else "mps" if torch.backends.mps.is_available() else "cpu"
print(f"Using {device} device")

# Define model
class NeuralNetwork(nn.Module):
    def __init__(self):
        super().__init__()
        self.flatten = nn.Flatten()
        self.linear_relu_stack = nn.Sequential(
            nn.Linear(28*28, 512),
            nn.ReLU(),
            nn.Linear(512, 512),
            nn.ReLU(),
            nn.Linear(512, 10)
        )

    def forward(self, x):
        x = self.flatten(x)
        logits = self.linear_relu_stack(x)
        return logits

model = NeuralNetwork().to(device)
print(model)
```

* Optimizing the Model Parameters
  * Need a __loss function__ and __optimiser__.

```
loss_fn = nn.CrossEntropyLoss()
optimizer = torch.optim.SGD(model.parameters(), lr=1e-3)
```
  * Train the model. In a single training loop, the model makes predictions on the training dataset (fed to it in batches), and backpropagates the prediction error to adjust the model’s parameters.

```
def train(dataloader, model, loss_fn, optimizer):
    size = len(dataloader.dataset)
    model.train()
    for batch, (X, y) in enumerate(dataloader):
        X, y = X.to(device), y.to(device)

        # Compute prediction error
        pred = model(X)
        loss = loss_fn(pred, y)

        # Backpropagation
        optimizer.zero_grad()
        loss.backward()
        optimizer.step()

        if batch % 100 == 0:
            loss, current = loss.item(), batch * len(X)
            print(f"loss: {loss:>7f}  [{current:>5d}/{size:>5d}]")
```

  * Test the model. We also check the model’s performance against the test dataset to ensure it is learning.

```
def test(dataloader, model, loss_fn):
    size = len(dataloader.dataset)
    num_batches = len(dataloader)
    model.eval()
    test_loss, correct = 0, 0
    with torch.no_grad():
        for X, y in dataloader:
            X, y = X.to(device), y.to(device)
            pred = model(X)
            test_loss += loss_fn(pred, y).item()
            correct += (pred.argmax(1) == y).type(torch.float).sum().item()
    test_loss /= num_batches
    correct /= size
    print(f"Test Error: \n Accuracy: {(100*correct):>0.1f}%, Avg loss: {test_loss:>8f} \n")
```

* Saving Models
  * A common way to save a model is to serialize the internal state dictionary (containing the model parameters).

```
torch.save(model.state_dict(), "model.pth")
print("Saved PyTorch Model State to model.pth")
```

* Loading Models
  * The process for loading a model includes re-creating the model structure and loading the state dictionary into it.

```
model = NeuralNetwork()
model.load_state_dict(torch.load("model.pth"))
```

```
classes = [
    "T-shirt/top",
    "Trouser",
    "Pullover",
    "Dress",
    "Coat",
    "Sandal",
    "Shirt",
    "Sneaker",
    "Bag",
    "Ankle boot",
]

model.eval()
x, y = test_data[0][0], test_data[0][1]
with torch.no_grad():
    pred = model(x)
    predicted, actual = classes[pred[0].argmax(0)], classes[y]
    print(f'Predicted: "{predicted}", Actual: "{actual}"')
```
# Appendix

This code should open up a window and show the first image in the test dataset.

```
transform = torch.transforms.ToPILImage()
image     = transform(test_data[0][0])
image.show()
```