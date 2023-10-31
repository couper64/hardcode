using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheet : MonoBehaviour
{
    public List<CharacterSheetInfo> listCharacterSheetInfo;

    [Space]
    public List<GameObject> prefabs;

    [Space]
    public List<CharacterSheet_InputField> listInputField;
    public List<CharacterSheet_Toggles> listToggles;

    [Space]
    public GameObject previousObject;

    /// <summary>
    /// I imagine, it should be called from a button assigned to a character.
    /// </summary>
    /// <param name="characterName"></param>
    public void OnCharacterSheetSelected(string characterName)
    {
        CharacterSheetInfo characterInfo = listCharacterSheetInfo.Find(m => m.characterName == characterName);

        GameObject namePrefab = prefabs.Find(m => m.name == "Name");
        GameObject nameObject = Instantiate(namePrefab, transform);

        GameObject levelPrefab = prefabs.Find(m => m.name == "Level");
        GameObject levelObject = Instantiate(levelPrefab, transform);

        GameObject xpPrefab = prefabs.Find(m => m.name == "XP");
        GameObject experienceObject = Instantiate(xpPrefab, transform);

        GameObject goldPrefab = prefabs.Find(m => m.name == "Gold");
        GameObject goldObject = Instantiate(goldPrefab, transform);

        // Resources:

        GameObject lumberPrefab = prefabs.Find(m => m.name == "Lumber");
        GameObject lumberObject = Instantiate(lumberPrefab, transform);

        GameObject ironPrefab = prefabs.Find(m => m.name == "Iron");
        GameObject ironObject = Instantiate(ironPrefab, transform);

        GameObject hidePrefab = prefabs.Find(m => m.name == "Hide");
        GameObject hideObject = Instantiate(hidePrefab, transform);



        GameObject arrowvinePrefab = prefabs.Find(m => m.name == "Arrowvine");
        GameObject arrowvineObject = Instantiate(arrowvinePrefab, transform);

        GameObject axenutPrefab = prefabs.Find(m => m.name == "Axenut");
        GameObject axenutObject = Instantiate(axenutPrefab, transform);

        GameObject corpscapPrefab = prefabs.Find(m => m.name == "Corpscap");
        GameObject corpscapObject = Instantiate(corpscapPrefab, transform);



        GameObject flamefruitPrefab = prefabs.Find(m => m.name == "Flamefruit");
        GameObject flamefruitObject = Instantiate(flamefruitPrefab, transform);

        GameObject rockrootPrefab = prefabs.Find(m => m.name == "Rockroot");
        GameObject rockrootObject = Instantiate(rockrootPrefab, transform);

        GameObject snowthistlePrefab = prefabs.Find(m => m.name == "Snowthistle");
        GameObject snowthistleObject = Instantiate(snowthistlePrefab, transform);

        // Notes:

        GameObject notesPrefab = prefabs.Find(m => m.name == "Notes");
        GameObject notesObject = Instantiate(notesPrefab, transform);

        // Masteries:

        GameObject masteryPrefab = prefabs.Find(m => m.name == "Mastery");
        GameObject masteryObject1 = Instantiate(masteryPrefab, transform);
        GameObject masteryObject2 = Instantiate(masteryPrefab, transform);

        masteryObject1.name = "Mastery1";
        masteryObject2.name = "Mastery2";

        CharacterSheet_Toggles masteryToggle1 = masteryObject1.GetComponent<CharacterSheet_Toggles>();
        CharacterSheet_Toggles masteryToggle2 = masteryObject2.GetComponent<CharacterSheet_Toggles>();

        masteryToggle1.labelName.text = "Mastery 1";
        masteryToggle2.labelName.text = "Mastery 2";

        // Ticks:

        GameObject ticksPrefab = prefabs.Find(m => m.name == "Ticks");
        GameObject ticksObject = Instantiate(ticksPrefab, transform);

        int perkCount = 1;
        foreach (var perk in characterInfo.perks)
        {
            GameObject prefab = prefabs.Find(m => m.name == "Perk");
            GameObject perkObject = Instantiate(prefab, transform);

            perkObject.name = $"Perk{perkCount}";

            CharacterSheet_Toggles toggles = perkObject.GetComponent<CharacterSheet_Toggles>();
            TextMeshProUGUI label = toggles.toggles[0].GetComponentInChildren<TextMeshProUGUI>();

            label.text = perk;

            toggles.labelName.text = $"Perk {perkCount++}";

            foreach (Toggle toggle in toggles.toggles)
            {
                toggle.isOn = PlayerPrefs.GetInt
                (
                    $"{characterInfo.characterName}.{toggles.name}",
                    0 // Default is Off.
                ) > 0 ? true : false;
            }

            foreach (Toggle toggle in toggles.toggles)
            {
                toggle.onValueChanged.AddListener((t) =>
                {
                    PlayerPrefs.SetInt
                    (
                        $"{characterInfo.characterName}.{toggle.name}", t == true ? 1 : 0
                    );
                });
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject childObject = transform.GetChild(i).gameObject;

            // Remove "(Clone)" part.
            childObject.name = childObject.name.Replace("(Clone)", "");

            // All off, except for the first one.
            childObject.SetActive(false);

            // The first element connects from outside.
            if (i == 0)
            {
                // All off, except for the first one.
                childObject.SetActive(true);
                GameObject perkNextObject = transform.GetChild(i + 1).gameObject;

                if (childObject.TryGetComponent(out CharacterSheet_InputField inputField))
                {
                    inputField.buttonBack.onClick.AddListener(() =>
                    {
                        previousObject.SetActive(true);

                        for (int j = transform.childCount - 1; j >= 0; j--)
                        {
                            Destroy(transform.GetChild(j).gameObject);
                        }
                    });

                    inputField.buttonNext.onClick.AddListener(() =>
                    {
                        childObject.SetActive(false);
                        perkNextObject.SetActive(true);
                    });
                }
                else if (childObject.TryGetComponent(out CharacterSheet_Toggles toggles))
                {
                    toggles.buttonBack.onClick.AddListener(() =>
                    {
                        previousObject.SetActive(true);

                        for (int j = transform.childCount - 1; j >= 0; j--)
                        {
                            Destroy(transform.GetChild(j).gameObject);
                        }
                    });

                    toggles.buttonNext.onClick.AddListener(() =>
                    {
                        childObject.SetActive(false);
                        perkNextObject.SetActive(true);
                    });
                }
            }

            // The last element connects to outside.
            else if (i == transform.childCount - 1)
            {
                GameObject perkBackObject = transform.GetChild(i - 1).gameObject;

                if (childObject.TryGetComponent(out CharacterSheet_InputField inputField))
                {
                    inputField.buttonBack.onClick.AddListener(() =>
                    {
                        perkBackObject.SetActive(true);
                        childObject.SetActive(false);
                    });

                    inputField.buttonNext.onClick.AddListener(() =>
                    {
                        // Do nothing.
                    });

                    TextMeshProUGUI label = inputField.buttonNext.GetComponentInChildren<TextMeshProUGUI>();
                    label.text = "End";
                }
                else if (childObject.TryGetComponent(out CharacterSheet_Toggles toggles))
                {
                    toggles.buttonBack.onClick.AddListener(() =>
                    {
                        perkBackObject.SetActive(true);
                        childObject.SetActive(false);
                    });

                    toggles.buttonNext.onClick.AddListener(() =>
                    {
                        // Do nothing.
                    });

                    TextMeshProUGUI label = toggles.buttonNext.GetComponentInChildren<TextMeshProUGUI>();
                    label.text = "End";
                }
            }

            // The rest are connected inside one-to-one.
            else
            {
                GameObject perkBackObject = transform.GetChild(i - 1).gameObject;
                GameObject perkNextObject = transform.GetChild(i + 1).gameObject;

                if (childObject.TryGetComponent(out CharacterSheet_InputField inputField))
                {
                    inputField.buttonBack.onClick.AddListener(() =>
                    {
                        perkBackObject.SetActive(true);
                        childObject.SetActive(false);
                    });

                    inputField.buttonNext.onClick.AddListener(() =>
                    {
                        childObject.SetActive(false);
                        perkNextObject.SetActive(true);
                    });
                }
                else if (childObject.TryGetComponent(out CharacterSheet_Toggles toggles))
                {
                    toggles.buttonBack.onClick.AddListener(() =>
                    {
                        perkBackObject.SetActive(true);
                        childObject.SetActive(false);
                    });

                    toggles.buttonNext.onClick.AddListener(() =>
                    {
                        childObject.SetActive(false);
                        perkNextObject.SetActive(true);
                    });
                }
            }
        } // for (int i = 0; i < transform.childCount; i++)

        // Setup Load/Save feature.

        // Name:

        nameObject.GetComponent<CharacterSheet_InputField>().inputField.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(nameObject)}", characterInfo.characterName);
        nameObject.GetComponent<CharacterSheet_InputField>().inputField.onValueChanged.AddListener((n) =>
        { PlayerPrefs.SetString($"{characterInfo.characterName}.{nameof(nameObject)}", n); });

        // Level:

        CharacterSheet_Toggles levelToggles = levelObject.GetComponent<CharacterSheet_Toggles>();
        foreach (var toggle in levelToggles.toggles)
        {
            toggle.isOn = PlayerPrefs.GetInt
            (
                $"{characterInfo.characterName}.{toggle.name}",
                0 // Default is Off.
            ) > 0 ? true : false;

            toggle.onValueChanged.AddListener((t) =>
            {
                PlayerPrefs.SetInt
                (
                    $"{characterInfo.characterName}.{toggle.name}", t == true ? 1 : 0
                );
            });
        }

        TMP_InputField experienceInput = experienceObject.GetComponent<CharacterSheet_InputField>().inputField;
        experienceInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(experienceInput)}", "0xp");
        TMP_InputField goldInput = goldObject.GetComponent<CharacterSheet_InputField>().inputField;
        goldInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(goldInput)}", "0g");

        TMP_InputField lumberInput = lumberObject.GetComponent<CharacterSheet_InputField>().inputField;
        lumberInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(lumberInput)}", "0");
        TMP_InputField ironInput = ironObject.GetComponent<CharacterSheet_InputField>().inputField;
        ironInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(ironInput)}", "0");
        TMP_InputField hideInput = hideObject.GetComponent<CharacterSheet_InputField>().inputField;
        hideInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(hideInput)}", "0");

        TMP_InputField axenutInput = axenutObject.GetComponent<CharacterSheet_InputField>().inputField;
        axenutInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(axenutInput)}", "0");
        TMP_InputField arrowvineInput = arrowvineObject.GetComponent<CharacterSheet_InputField>().inputField;
        arrowvineInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(arrowvineInput)}", "0");
        TMP_InputField corpscapInput = corpscapObject.GetComponent<CharacterSheet_InputField>().inputField;
        corpscapInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(corpscapInput)}", "0");

        TMP_InputField snowthistleInput = snowthistleObject.GetComponent<CharacterSheet_InputField>().inputField;
        snowthistleInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(snowthistleInput)}", "0");
        TMP_InputField flamefruitInput = flamefruitObject.GetComponent<CharacterSheet_InputField>().inputField;
        flamefruitInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(flamefruitInput)}", "0");
        TMP_InputField rockrootInput = rockrootObject.GetComponent<CharacterSheet_InputField>().inputField;
        rockrootInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(rockrootInput)}", "0");

        TMP_InputField notesInput = notesObject.GetComponent<CharacterSheet_InputField>().inputField;
        notesInput.text = PlayerPrefs
            .GetString($"{characterInfo.characterName}.{nameof(notesInput)}", "No notes");

        CharacterSheet_Toggles masteryToggles1 = masteryObject1.GetComponent<CharacterSheet_Toggles>();
        foreach (Toggle toggle in masteryToggles1.toggles)
        {
            toggle.isOn = PlayerPrefs.GetInt
            (
                $"{characterInfo.characterName}.{masteryToggles1.name}",
                0 // Default is Off.
            ) > 0 ? true : false;
        }

        CharacterSheet_Toggles masteryToggles2 = masteryObject2.GetComponent<CharacterSheet_Toggles>();
        foreach (Toggle toggle in masteryToggles2.toggles)
        {
            toggle.isOn = PlayerPrefs.GetInt
            (
                $"{characterInfo.characterName}.{masteryToggles2.name}",
                0 // Default is Off.
            ) > 0 ? true : false;
        }



        CharacterSheet_Toggles ticksToggles = ticksObject.GetComponent<CharacterSheet_Toggles>();
        foreach (Toggle toggle in ticksToggles.toggles)
        {
            toggle.isOn = PlayerPrefs.GetInt
            (
                $"{characterInfo.characterName}.{toggle.name}",
                0 // Default is Off.
            ) > 0 ? true : false;
        }

        experienceInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(experienceInput)}", n
            );
        });
        goldInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(goldInput)}", n
            );
        });

        lumberInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(lumberInput)}", n
            );
        });
        ironInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(ironInput)}", n
            );
        });
        hideInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(hideInput)}", n
            );
        });

        axenutInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(axenutInput)}", n
            );
        });
        arrowvineInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(arrowvineInput)}", n
            );
        });
        corpscapInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(corpscapInput)}", n
            );
        });
        snowthistleInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(snowthistleInput)}", n
            );
        });
        flamefruitInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(flamefruitInput)}", n
            );
        });
        rockrootInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(rockrootInput)}", n
            );
        });

        notesInput.onEndEdit.AddListener((n) =>
        {
            PlayerPrefs.SetString
            (
                $"{characterInfo.characterName}.{nameof(notesInput)}", n
            );
        });

        foreach (Toggle toggle in masteryToggles1.toggles)
        {
            toggle.onValueChanged.AddListener((t) =>
            {
                PlayerPrefs.SetInt
                (
                    $"{characterInfo.characterName}.{masteryToggles1.name}", t == true ? 1 : 0
                );
            });
        }

        foreach (Toggle toggle in masteryToggles2.toggles)
        {
            toggle.onValueChanged.AddListener((t) =>
            {
                PlayerPrefs.SetInt
                (
                    $"{characterInfo.characterName}.{masteryToggles2.name}", t == true ? 1 : 0
                );
            });
        }



        foreach (Toggle toggle in ticksToggles.toggles)
        {
            toggle.onValueChanged.AddListener((t) =>
            {
                PlayerPrefs.SetInt
                (
                    $"{characterInfo.characterName}.{toggle.name}", t == true ? 1 : 0
                );
            });
        }
    }
}
