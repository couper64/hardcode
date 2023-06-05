import torch as torch

def main():
    input : torch.Tensor = torch.ones(1, 3, 32, 32)
    print(input)
    model : torch.nn.Conv2d = torch.nn.Conv2d(3, 32, 3)
    # print(model)
    prediction : torch.Tensor = model(input)
    print(prediction)

if __name__ == "__main__":
    main()