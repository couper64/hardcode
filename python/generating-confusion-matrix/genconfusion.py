import numpy as np
import matplotlib.pyplot as plt

def calc_confusionMatrix():

    confusionMatrix = [[1290, 129, 273, 99, 774],
                      [41, 17765 , 92, 523, 7018],
                      [201, 209, 789, 122, 990],
                      [63, 2366, 149, 1308, 1479],
                      [430, 5075, 731, 407, 0]]

    labelNamesPred = ["bus","car","truck","van","False Negative"]
    labelNamesTruth = ["bus","car","truck","van","False Positive"]

    confusionMatrix = np.array(confusionMatrix)

    # plot confusion matrix
    plotsizing = max(0.8*4, 7)
    fig = plt.figure(figsize=(plotsizing,plotsizing))
    plt.rcParams['xtick.bottom'] = plt.rcParams['xtick.labelbottom'] = False
    plt.rcParams['xtick.top'] = plt.rcParams['xtick.labeltop'] = True
    plt.imshow(confusionMatrix, cmap='Blues', origin='upper')
    plt.xticks(np.arange(len(labelNamesPred)), labels=labelNamesPred, rotation=90)
    plt.yticks(np.arange(len(labelNamesTruth)), labels=labelNamesTruth)
    plt.colorbar()
    fig.tight_layout()

    # add values in the boxes
    thresh = confusionMatrix.max() / 2.
    for i in range(confusionMatrix.shape[0]):
        for j in range(confusionMatrix.shape[1]):
            plt.text(j, i, int(confusionMatrix[i, j]),
                     ha="center", va="center",
                     color="white" if confusionMatrix[i, j] == 0 or confusionMatrix[i, j] > thresh else "black")

            # plt.show()
    plt.savefig("confusionMatrix.jpg", dpi=600)
    np.savetxt("confusionMatrixData.txt", confusionMatrix, fmt="%d")

if __name__ == "__main__":
    calc_confusionMatrix()