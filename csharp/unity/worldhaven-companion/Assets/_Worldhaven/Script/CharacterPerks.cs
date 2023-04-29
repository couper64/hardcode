using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PeasantsLogic
{
    public class CharacterPerks : MonoBehaviour
    {
        public string playerPrefId;

        [Space]
        public TMP_InputField nameInput;

        [Space]
        public List<Toggle> levelToggles;

        [Space]
        public TMP_InputField experienceInput;
        public TMP_InputField goldInput;

        [Space]
        public TMP_InputField woodInput;
        public TMP_InputField arrowvineInput;
        public TMP_InputField flamefruitInput;

        [Space]
        public TMP_InputField ironInput;
        public TMP_InputField axenutInput;
        public TMP_InputField rockrootInput;

        [Space]
        public TMP_InputField hideInput;
        public TMP_InputField corpsecapInput;
        public TMP_InputField snowthistleInput;

        [Space]
        public TMP_InputField notesInput;

        [Space]
        public List<Toggle> masteryToggles;

        [Space]
        public List<Toggle> perkToggles;

        [Space]
        public List<Toggle> tickToggles;


        private void Start()
        {
            nameInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(nameInput)}", playerPrefId);

            foreach (Toggle toggle in levelToggles)
            {
                toggle.isOn = PlayerPrefs.GetInt
                (
                    $"{playerPrefId}.{toggle.name}",
                    0 // Default is Off.
                ) > 0 ? true : false;
            }

            experienceInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(experienceInput)}", "0xp");
            goldInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(goldInput)}", "0g");

            woodInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(woodInput)}", "0");
            ironInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(ironInput)}", "0");
            hideInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(hideInput)}", "0");

            axenutInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(axenutInput)}", "0");
            arrowvineInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(arrowvineInput)}", "0");
            corpsecapInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(corpsecapInput)}", "0");

            snowthistleInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(snowthistleInput)}", "0");
            flamefruitInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(flamefruitInput)}", "0");
            rockrootInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(rockrootInput)}", "0");

            notesInput.text = PlayerPrefs
                .GetString($"{playerPrefId}.{nameof(notesInput)}", "0");


            foreach (Toggle toggle in masteryToggles)
            {
                toggle.isOn = PlayerPrefs.GetInt
                (
                    $"{playerPrefId}.{toggle.name}",
                    0 // Default is Off.
                ) > 0 ? true : false;
            }

            foreach (Toggle toggle in perkToggles)
            {
                toggle.isOn = PlayerPrefs.GetInt
                (
                    $"{playerPrefId}.{toggle.name}",
                    0 // Default is Off.
                ) > 0 ? true : false;
            }

            foreach (Toggle toggle in tickToggles)
            {
                toggle.isOn = PlayerPrefs.GetInt
                (
                    $"{playerPrefId}.{toggle.name}",
                    0 // Default is Off.
                ) > 0 ? true : false;
            }

            nameInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString($"{playerPrefId}.{nameof(nameInput)}", n);
            });

            foreach (Toggle toggle in levelToggles)
            {
                toggle.onValueChanged.AddListener((t) => 
                {
                    PlayerPrefs.SetInt
                    (
                        $"{playerPrefId}.{toggle.name}", t == true ? 1 : 0
                    );
                });
            }

            experienceInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(experienceInput)}", n
                );
            });
            goldInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(goldInput)}", n
                );
            });

            woodInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(woodInput)}", n
                );
            });
            ironInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(ironInput)}", n
                );
            });
            hideInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(hideInput)}", n
                );
            });

            axenutInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(axenutInput)}", n
                );
            });
            arrowvineInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(arrowvineInput)}", n
                );
            });
            corpsecapInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(corpsecapInput)}", n
                );
            });
            snowthistleInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(snowthistleInput)}", n
                );
            });
            flamefruitInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(flamefruitInput)}", n
                );
            });
            rockrootInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(rockrootInput)}", n
                );
            });

            notesInput.onEndEdit.AddListener((n) =>
            {
                PlayerPrefs.SetString
                (
                    $"{playerPrefId}.{nameof(notesInput)}", n
                );
            });

            foreach (Toggle toggle in masteryToggles)
            {
                toggle.onValueChanged.AddListener((t) => 
                {
                    PlayerPrefs.SetInt
                    (
                        $"{playerPrefId}.{toggle.name}", t == true ? 1 : 0
                    );
                });
            }

            foreach (Toggle toggle in perkToggles)
            {
                toggle.onValueChanged.AddListener((t) => 
                {
                    PlayerPrefs.SetInt
                    (
                        $"{playerPrefId}.{toggle.name}", t == true ? 1 : 0
                    );
                });
            }

            foreach (Toggle toggle in tickToggles)
            {
                toggle.onValueChanged.AddListener((t) => 
                {
                    PlayerPrefs.SetInt
                    (
                        $"{playerPrefId}.{toggle.name}", t == true ? 1 : 0
                    );
                });
            }
        }
    } // CharacterPerks
} // PeasantsLogic
