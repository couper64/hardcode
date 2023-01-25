using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum EGameState
{
    PLAYING,
    LOBBY_MAIN,
    LOBBY_ALLIANCE,
    LOBBY_HANGAR_MAIN,
    LOBBY_HANGAR_UGRADES,
    LOBBY_RANKING_GLOBAL,
    LOBBY_RANKING_REGION,
    LOBBY_RANKING_FRIENDS,
    LOBBY_REWARDS_DAILY,
    LOBBY_REWARDS_WATCH,
    LOBBY_REWARDS_FRIENDS,
    LOBBY_SHOP
}

public class HUD : MonoBehaviour
{
    private EGameState GameState;

    /// <summary>
    /// Main and Hangar Lobbies retrieve 
    /// automatically on Start.
    /// Lobbies bellow retrieved manually 
    /// in the editor.
    /// </summary>
    #region Lobbies

    [Header("Main lobbies")]
    [SerializeField]
    private GameObject Lobby_Main;

    [SerializeField]
    private GameObject Lobby_Alliance;

    [SerializeField]
    private GameObject Lobby_Hangar;

    [SerializeField]
    private GameObject Lobby_Ranking;

    [SerializeField]
    private GameObject Lobby_Rewards;

    [SerializeField]
    private GameObject Lobby_Shop;

    /// <summary>
    /// Objects belongs to hangar
    /// </summary>
    #region Hangar
    [Header("Sub-objects from Hangar")]
    [SerializeField]
    private GameObject Lobby_Hangar_MainContent;

    [SerializeField]
    private GameObject Lobby_Hangar_Upgrades;

    [SerializeField]
    private Text Lobby_Hangar_VehicleName;

    [SerializeField]
    private UnityEngine.UI.Button Lobby_Hangar_BTN_Ships;

    [SerializeField]
    private UnityEngine.UI.Button Lobby_Hangar_BTN_Drones;

    [SerializeField]
    private UnityEngine.UI.Button Lobby_Hangar_BTN_Selected;

    [SerializeField]
    private Text Lobby_Hangar_VehicleDescription;

    [SerializeField]
    private Text Lobby_Hangar_VehicleCurrentDamage;

    [SerializeField]
    private Text Lobby_Hangar_VehicleCurrentFireRate;

    [SerializeField]
    private Text Lobby_Hangar_VehicleCurrentShield;

    [SerializeField]
    private Text Lobby_Hangar_VehicleCurrentMagneticField;
    #endregion

    /// <summary>
    /// Objects belongs to ranking
    /// </summary>
    #region Ranking
    [Header("Sub-objects from Ranking")]
    [SerializeField]
    private UnityEngine.UI.Button Lobby_Ranking_BTN_Global;

    [SerializeField]
    private UnityEngine.UI.Button Lobby_Ranking_BTN_Region;

    [SerializeField]
    private UnityEngine.UI.Button Lobby_Ranking_BTN_Friends;

    [SerializeField]
    private GameObject Lobby_Ranking_Content_Region;

    [SerializeField]
    private GameObject Lobby_Ranking_Content_Global;

    [SerializeField]
    private GameObject Lobby_Ranking_Content_Friends;

    #endregion

    /// <summary>
    /// Objects belongs to rewards
    /// </summary>
    #region Rewards
    [Header("Sub-objects from Rewards")]
    [SerializeField]
    private UnityEngine.UI.Button Lobby_Rewards_BTN_Daily;

    [SerializeField]
    private UnityEngine.UI.Button Lobby_Rewards_BTN_Watch;

    [SerializeField]
    private UnityEngine.UI.Button Lobby_Rewards_BTN_Friends;

    [SerializeField]
    private GameObject Lobby_Rewards_Content_Daily;

    [SerializeField]
    private GameObject Lobby_Rewards_Content_Watch;

    [SerializeField]
    private GameObject Lobby_Rewards_Content_Friends;

    #endregion


    [Header("Color block for selected button")]
    [SerializeField]
    ColorBlock SelectedButtonColorBlock;

    [Header("Color block for not selected button")]
    [SerializeField]
    ColorBlock NotSelectedButtonColorBlock;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GameState = EGameState.LOBBY_MAIN;
      //  SetUpReferences();
    }

    #region Enable/Disable
    public void EnableLobby_Main()
    {
        Lobby_Main.SetActive(true);
        GameState = EGameState.LOBBY_MAIN;
    }

    public void DissableLobby_Main()
    {
        Lobby_Main.SetActive(false);
    }

    public void EnableLobby_Hangar()
    {
        Lobby_Hangar.SetActive(true);
        EnableLobby_Hangar_Main();
        GameState = EGameState.LOBBY_HANGAR_MAIN;
    }

    public void DissableLobby_Hangar()
    {
        Lobby_Hangar.SetActive(false);
    }

    public void EnableLobby_Hangar_Upgrades()
    {
        Lobby_Hangar_Upgrades.SetActive(true);
        GameState = EGameState.LOBBY_HANGAR_UGRADES;
    }

    public void DissableLobby_Hangar_Upgrades()
    {
        Lobby_Hangar_Upgrades.SetActive(false);
    }

    public void EnableLobby_Hangar_Main()
    {
        Lobby_Hangar_MainContent.SetActive(true);
        GameState = EGameState.LOBBY_HANGAR_UGRADES;
    }

    public void DissableLobby_Hangar_Main()
    {
        Lobby_Hangar_MainContent.SetActive(false);
    }

    public void EnableLobby_Shop()
    {
        Lobby_Shop.SetActive(true);
        GameState = EGameState.LOBBY_SHOP;
    }

    public void DissableLobby_Shop()
    {
        Lobby_Shop.SetActive(false);
    }

    #region Rewards
    public void EnableLobby_Rewards()
    {
        Lobby_Rewards.SetActive(true);
        EnableLobby_Rewards_Daily();
    }

    public void DissableLobby_Rewards()
    {
        switch (GameState)
        {
            case EGameState.LOBBY_REWARDS_DAILY: DissableLobby_Rewards_Daily(); break;
            case EGameState.LOBBY_REWARDS_WATCH: DissableLobby_Rewards_Watch(); break;
            case EGameState.LOBBY_REWARDS_FRIENDS: DissableLobby_Rewards_Friends(); break;
        }
        Lobby_Rewards.SetActive(false);
    }


    public void EnableLobby_Rewards_Daily()
    {
        if (GameState != EGameState.LOBBY_REWARDS_DAILY)
        {
            switch (GameState)
            {
                case EGameState.LOBBY_REWARDS_WATCH: DissableLobby_Rewards_Watch(); break;
                case EGameState.LOBBY_REWARDS_FRIENDS: DissableLobby_Rewards_Friends(); break;
            }

            GameState = EGameState.LOBBY_REWARDS_DAILY;
            Lobby_Rewards_Content_Daily.SetActive(true);
            Lobby_Rewards_BTN_Daily.colors = SelectedButtonColorBlock;
        }
    }

    public void DissableLobby_Rewards_Daily()
    {
        Lobby_Rewards_Content_Daily.SetActive(false);
        Lobby_Rewards_BTN_Daily.colors = NotSelectedButtonColorBlock;
    }

    public void EnableLobby_Rewards_Watch()
    {
        if (GameState != EGameState.LOBBY_REWARDS_WATCH)
        {
            switch (GameState)
            {
                case EGameState.LOBBY_REWARDS_DAILY: DissableLobby_Rewards_Daily(); break;
                case EGameState.LOBBY_REWARDS_FRIENDS: DissableLobby_Rewards_Friends(); break;
            }

            GameState = EGameState.LOBBY_REWARDS_WATCH;
            Lobby_Rewards_Content_Watch.SetActive(true);
            Lobby_Rewards_BTN_Watch.colors = SelectedButtonColorBlock;
        }
    }

    public void DissableLobby_Rewards_Watch()
    {
        Lobby_Rewards_Content_Watch.SetActive(false);
        Lobby_Rewards_BTN_Watch.colors = NotSelectedButtonColorBlock;
    }

    public void EnableLobby_Rewards_Friends()
    {
        if (GameState != EGameState.LOBBY_REWARDS_FRIENDS)
        {
            switch (GameState)
            {
                case EGameState.LOBBY_REWARDS_DAILY:    DissableLobby_Rewards_Daily();      break;
                case EGameState.LOBBY_REWARDS_WATCH:    DissableLobby_Rewards_Watch();      break;
            }

            GameState = EGameState.LOBBY_REWARDS_FRIENDS;
            Lobby_Rewards_Content_Friends.SetActive(true);
            Lobby_Rewards_BTN_Friends.colors = SelectedButtonColorBlock;
        }
    }

    public void DissableLobby_Rewards_Friends()
    {
        Lobby_Rewards_Content_Friends.SetActive(false);
        Lobby_Rewards_BTN_Friends.colors = NotSelectedButtonColorBlock;
    }
    #endregion

    #region Ranking
    public void EnableLobby_Ranking()
    {
        Lobby_Ranking.SetActive(true);
        EnableLobby_Ranking_Region();
    }

    public void DissableLobby_Ranking()
    {
        switch (GameState)
        {
            case EGameState.LOBBY_RANKING_GLOBAL: DissableLobby_Ranking_Global(); break;
            case EGameState.LOBBY_RANKING_REGION: DissableLobby_Ranking_Region(); break;
            case EGameState.LOBBY_RANKING_FRIENDS: DissableLobby_Ranking_Friends(); break;
        }
        Lobby_Ranking.SetActive(false);
    }

    public void EnableLobby_Ranking_Global()
    {
        if (GameState != EGameState.LOBBY_RANKING_GLOBAL)
        {
            switch (GameState)
            {
                case EGameState.LOBBY_RANKING_REGION: DissableLobby_Ranking_Region(); break;
                case EGameState.LOBBY_RANKING_FRIENDS: DissableLobby_Ranking_Friends(); break;
            }
            GameState = EGameState.LOBBY_RANKING_GLOBAL;
            Lobby_Ranking_BTN_Global.colors = SelectedButtonColorBlock;
            Lobby_Ranking_Content_Global.SetActive(true);
        }
    }

    public void DissableLobby_Ranking_Global()
    {
        Lobby_Ranking_BTN_Global.colors = NotSelectedButtonColorBlock;
        Lobby_Ranking_Content_Global.SetActive(false);
    }

    public void EnableLobby_Ranking_Region()
    {
        if (GameState != EGameState.LOBBY_RANKING_REGION)
        {
            switch (GameState)
            {
                case EGameState.LOBBY_RANKING_GLOBAL: DissableLobby_Ranking_Global(); break;
                case EGameState.LOBBY_RANKING_FRIENDS: DissableLobby_Ranking_Friends(); break;
            }
            GameState = EGameState.LOBBY_RANKING_REGION;
            Lobby_Ranking_BTN_Region.colors = SelectedButtonColorBlock;
            Lobby_Ranking_Content_Region.SetActive(true);
        }
    }

    public void DissableLobby_Ranking_Region()
    {
        Lobby_Ranking_BTN_Region.colors = NotSelectedButtonColorBlock;
        Lobby_Ranking_Content_Region.SetActive(false);
    }

    public void EnableLobby_Ranking_Friends()
    {
        if (GameState != EGameState.LOBBY_RANKING_FRIENDS)
        {
            switch (GameState)
            {
                case EGameState.LOBBY_RANKING_GLOBAL: DissableLobby_Ranking_Global(); break;
                case EGameState.LOBBY_RANKING_REGION: DissableLobby_Ranking_Region(); break;
            }
            GameState = EGameState.LOBBY_RANKING_FRIENDS;
            Lobby_Ranking_BTN_Friends.colors = SelectedButtonColorBlock;
            Lobby_Ranking_Content_Friends.SetActive(true);
        }
    }

    public void DissableLobby_Ranking_Friends()
    {
        Lobby_Ranking_BTN_Friends.colors = NotSelectedButtonColorBlock;
        Lobby_Ranking_Content_Friends.SetActive(false);
    }
    #endregion

    public void LobbyOneStepBack()
    {
        switch (GameState)
        {
            case EGameState.LOBBY_ALLIANCE:                 break;
            case EGameState.LOBBY_HANGAR_MAIN:          DissableLobby_Hangar(); EnableLobby_Main(); break;
            case EGameState.LOBBY_HANGAR_UGRADES:       DissableLobby_Hangar_Upgrades(); EnableLobby_Hangar(); break;
            case EGameState.LOBBY_RANKING_GLOBAL:       DissableLobby_Ranking(); EnableLobby_Main(); break;
            case EGameState.LOBBY_RANKING_REGION:       DissableLobby_Ranking(); EnableLobby_Main(); break;
            case EGameState.LOBBY_RANKING_FRIENDS:      DissableLobby_Ranking(); EnableLobby_Main(); break;
            case EGameState.LOBBY_REWARDS_DAILY:        DissableLobby_Rewards(); EnableLobby_Main(); break;
            case EGameState.LOBBY_REWARDS_WATCH:        DissableLobby_Rewards(); EnableLobby_Main(); break;
            case EGameState.LOBBY_REWARDS_FRIENDS:      DissableLobby_Rewards(); EnableLobby_Main(); break;
            case EGameState.LOBBY_SHOP:                 DissableLobby_Shop(); EnableLobby_Main(); break;
        }
    }

    public void BackToLobbyMain()
    {
        switch (GameState)
        {
            case EGameState.LOBBY_ALLIANCE:                 break;
            case EGameState.LOBBY_HANGAR_MAIN:          DissableLobby_Hangar(); EnableLobby_Main(); break;
            case EGameState.LOBBY_HANGAR_UGRADES:       DissableLobby_Hangar(); DissableLobby_Hangar_Upgrades(); EnableLobby_Main(); break;
            case EGameState.LOBBY_RANKING_GLOBAL:       DissableLobby_Ranking(); EnableLobby_Main(); break;
            case EGameState.LOBBY_RANKING_REGION:       DissableLobby_Ranking(); EnableLobby_Main(); break;
            case EGameState.LOBBY_RANKING_FRIENDS:      DissableLobby_Ranking(); EnableLobby_Main(); break;
            case EGameState.LOBBY_REWARDS_DAILY:        DissableLobby_Rewards(); EnableLobby_Main(); break;
            case EGameState.LOBBY_REWARDS_WATCH:        DissableLobby_Rewards(); EnableLobby_Main(); break;
            case EGameState.LOBBY_REWARDS_FRIENDS:      DissableLobby_Rewards(); EnableLobby_Main(); break;
            case EGameState.LOBBY_SHOP:                 DissableLobby_Shop(); EnableLobby_Main(); break;
        }
    }

    public void DisableAllLobies() 
    {
        Lobby_Main.SetActive(false);
        Lobby_Alliance.SetActive(false);
        Lobby_Rewards.SetActive(false);
        Lobby_Hangar.SetActive(false);
        Lobby_Hangar_Upgrades.SetActive(false);
        Lobby_Ranking.SetActive(false);
        Lobby_Shop.SetActive(false);
    }

    public void EnablePlayingState() 
    {
        GameState = EGameState.PLAYING;
    }
    #endregion

    #region Setters
    // DEPRICATED ! DELETE ? Not in used, each reference is assigned via editor
    // DEPRICATED ! DELETE ? Not in used, each reference is assigned via editor
    // DEPRICATED ! DELETE ? Not in used, each reference is assigned via editor
    // DEPRICATED ! DELETE ? Not in used, each reference is assigned via editor
    private void SetUpReferences()
    {
        Canvas MainCanvas = FindObjectOfType<Canvas>();

#if UNITY_EDITOR
        if (MainCanvas == null) Debug.Log("ERROR: MainCanvas is not valid, printed from HUD.cs");
#endif

        List<Transform> tempMainCanvasChildrens = new List<Transform>();

        for (int i = 0; i < MainCanvas.transform.childCount; i++)
        {
            tempMainCanvasChildrens.Add(MainCanvas.transform.GetChild(i));
        }

        foreach (Transform child in tempMainCanvasChildrens)
        {
            if (child.name == "Lobby_Main")
            {
                Lobby_Main = child.gameObject;
            }
            else if (child.name == "Lobby_Hangar")
            {
                Lobby_Hangar = child.gameObject;

                List<Transform> tempParrent = new List<Transform>();

                for (int i = 0; i < Lobby_Hangar.transform.childCount; i++)
                {
                    tempParrent.Add(Lobby_Hangar.transform.GetChild(i));
                }

                foreach (Transform childOfChild in tempParrent)
                {
                    if (childOfChild.name == "MainContent")
                    {
                        Lobby_Hangar_MainContent = childOfChild.gameObject;

                        List<Transform> temParrentMainContent = new List<Transform>();

                        for (int i = 0; i < Lobby_Hangar.transform.childCount; i++)
                        {
                            temParrentMainContent.Add(Lobby_Hangar_MainContent.transform.GetChild(i));
                        }
                        foreach (Transform childOfMainContent in temParrentMainContent)
                        {
                            if (childOfMainContent.name == "Ship_Description")
                            {
                                List<Transform> tempParrentShipDescription = new List<Transform>();

                                for (int i = 0; i < childOfMainContent.transform.childCount; i++)
                                {
                                    tempParrentShipDescription.Add(childOfMainContent.transform.GetChild(i));
                                }

                                foreach (Transform childOfShipDescription in tempParrentShipDescription)
                                {
                                    if (childOfShipDescription.name == "TXT_Name")
                                    {
                                        Lobby_Hangar_VehicleName = childOfShipDescription.GetComponent<Text>();
                                    }
                                    else if (childOfShipDescription.name == "TXT_Description")
                                    {
                                        Lobby_Hangar_VehicleDescription = childOfShipDescription.GetComponent<Text>();
                                    }
                                    else if (childOfShipDescription.name == "TXT_Damage_Value")
                                    {
                                        Lobby_Hangar_VehicleCurrentDamage = childOfShipDescription.GetComponent<Text>();
                                    }
                                    else if (childOfShipDescription.name == "TXT_FireRate_Value")
                                    {
                                        Lobby_Hangar_VehicleCurrentFireRate = childOfShipDescription.GetComponent<Text>();
                                    }
                                    else if (childOfShipDescription.name == "TXT_Shield_Value")
                                    {
                                        Lobby_Hangar_VehicleCurrentShield = childOfShipDescription.GetComponent<Text>();
                                    }
                                    else if (childOfShipDescription.name == "TXT_MagneticField_Value")
                                    {
                                        Lobby_Hangar_VehicleCurrentMagneticField = childOfShipDescription.GetComponent<Text>();
                                    }
                                }
                            }
                        }
                    }
                    else if (childOfChild.name == "Upgrades")
                    {
                        Lobby_Hangar_Upgrades = childOfChild.gameObject;
                    }
                }
            }
        }
    }

    // Setters
    public void SetCurrentVehicleName(string newName) { Lobby_Hangar_VehicleName.text = newName; }
    public void SetCurrentVehicleDescription(string newDescription) { Lobby_Hangar_VehicleDescription.text = newDescription; }
    public void SetCurrentVehicleDamage(int newValue) { Lobby_Hangar_VehicleCurrentDamage.text = newValue.ToString(); }
    public void SetCurrentVehicleFireRate(int newValue) { Lobby_Hangar_VehicleCurrentFireRate.text = newValue.ToString(); }
    public void SetCurrentVehicleShield(int newValue) { Lobby_Hangar_VehicleCurrentShield.text = newValue.ToString(); }
    public void SetCurrentVehicleMagneticField(int newValue) { Lobby_Hangar_VehicleCurrentMagneticField.text = newValue.ToString(); }
#endregion

}

