using UnityEngine;

public class BalanceHUD : MonoBehaviour
{
    [Header("References.")]
    [SerializeField]
    private LevelProgressor levelProgressor;

    [Header("Balance Labels.")]
    [SerializeField]
    private TMPro.TextMeshProUGUI balanceLabel;

    private void Reset()
    {
        // Defaults.
        levelProgressor = null;
        balanceLabel = null;
    }

    private void Update()
    {
        // New balance.
        balanceLabel.text = levelProgressor.playerData.balance.ToString();
    }
}
