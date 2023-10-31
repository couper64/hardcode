using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CharacterSheet_Toggles : MonoBehaviour
{
    public Button buttonNext;
    public Button buttonBack;

    [Space]
    public TextMeshProUGUI labelName;

    [Space]
    public List<Toggle> toggles;
}
