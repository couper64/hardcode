using UnityEngine;

public class LevelOver : MonoBehaviour
{
    public enum State
    {
        Lose = 0x0001,
        Win = 0x0002,
        Undefined = 0xFFFF // 65 535 - Maximum.
    }

    [Header("State of the Panel")]
    [SerializeField]
    // Its public because accessed from outside.
    public State state;

    [Header("UI elements under control.")]
    [SerializeField]
    private TMPro.TextMeshProUGUI loseLabel;
    [SerializeField]
    private TMPro.TextMeshProUGUI winLabel;
    [SerializeField]
    private TMPro.TextMeshProUGUI balanceLabel;

    [Header("Level Progressor of the scene.")]
    [SerializeField]
    private LevelProgressor levelProgressor;

    private void Reset()
    {
        // Defaults.
        state = State.Undefined;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Lose:
                ShowLose();
                break;
            case State.Win:
                ShowWin();
                break;
            case State.Undefined:
                HideAll();
                break;
            default:
                break;
        }
    }

    private void ShowLose()
    {
        // Hide by disabling. Checking.
        if (winLabel.gameObject.activeSelf)
        {
            // And, action.
            winLabel.gameObject.SetActive(false);
        }

        // Show by enabling. Checking.
        if (!loseLabel.gameObject.activeSelf)
        {
            // And, action.
            loseLabel.gameObject.SetActive(true);
        }

        // Show player's new balance.
        string balance = "Balance: ";
        balance += levelProgressor.playerData.balance.ToString();

        // Store it in label.
        balanceLabel.text = balance;
    }

    private void ShowWin()
    {
        // Hide by disabling. Checking.
        if (loseLabel.gameObject.activeSelf)
        {
            // And, action.
            loseLabel.gameObject.SetActive(false);
        }

        // Show by enabling. Checking.
        if (!winLabel.gameObject.activeSelf)
        {
            // And, action.
            winLabel.gameObject.SetActive(true);
        }

        // Show player's new balance.
        string balance = "Balance: ";
        balance += levelProgressor.playerData.balance.ToString();

        // Store it in label.
        balanceLabel.text = balance;
    }

    private void HideAll()
    {
        // Hide by disabling. Checking.
        if (loseLabel.gameObject.activeSelf)
        {
            // And, action.
            loseLabel.gameObject.SetActive(false);
        }

        // Hide by disabling. Checking.
        if (winLabel.gameObject.activeSelf)
        {
            // And, action.
            winLabel.gameObject.SetActive(false);
        }
    }
}
