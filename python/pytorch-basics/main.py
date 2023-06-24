import torch as torch

def main():
    input : torch.Tensor = torch.ones(1, 3, 800, 600)
    # print(input)
    conv2d : torch.nn.Conv2d = torch.nn.Conv2d(3, 32, 7)
    # print(model)
    sigmoid : torch.nn.Sigmoid = torch.nn.Sigmoid()
    pool : torch.nn.AvgPool2d = torch.nn.AvgPool2d(2)
    prediction : torch.Tensor = pool(sigmoid(conv2d(input)))
    print(prediction)

if __name__ == "__main__":
    main()