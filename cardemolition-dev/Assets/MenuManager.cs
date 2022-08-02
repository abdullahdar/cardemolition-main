using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("MENUS")]
    [SerializeField]
    private Canvas mainMenu;
    [SerializeField]
    private Canvas levelSelection;
    [SerializeField]
    private Canvas carSelection;
    [SerializeField]
    private Canvas loading;
    public enum SelectedMenu
    {
        _mainMenu,
        _levelSelection,
        _carSelection,
        _loading
    }
    public SelectedMenu selectedMenu;

    [Header("GARAGE THINGS")]
    //public MeshRenderer garage;
    public GameObject cars;

    [Header("TOP BAR")]
    public Text txtCoins;

    [Header("REFERENCES")]
    private LevelLoader levelLoader;
    private LevelSelection levelSelectionScript;
    private CarSelection carSelectionScript;
    private LevelsData levelsData;

    [SerializeField]
    AudioManager audioManager;

    private void Awake()
    {
        levelLoader = GetComponent<LevelLoader>();
        levelSelectionScript = GetComponent<LevelSelection>();
        carSelectionScript = GetComponent<CarSelection>();
        levelsData = levelSelectionScript.levelsData;
        if (levelsData.openCarSelection)
        {
            Show_CarSelection();
            levelsData.openCarSelection = false;
        }
        else
            Show_MainMenu();
                
    }

    private void Start()
    {
        audioManager.Play("menuMusic");        
    }

    void OpenMenu(SelectedMenu _selectedMenu)
    {
        Hide_Selected_Menu();
        switch (_selectedMenu)
        {
            case SelectedMenu._mainMenu:
                mainMenu.enabled = true;
                selectedMenu = _selectedMenu;
                break;
            case SelectedMenu._levelSelection:
                levelSelection.enabled = true;                
                selectedMenu = _selectedMenu;
                break;
            case SelectedMenu._carSelection:
                carSelection.enabled = true;                
                selectedMenu = _selectedMenu;
                EnableDisable_Garage(true);
                break;
            case SelectedMenu._loading:
                loading.enabled = true;                
                selectedMenu = _selectedMenu;
                break;            
            default:
                break;
        }
    }
    void Hide_Selected_Menu()
    {
        switch (selectedMenu)
        {
            case SelectedMenu._mainMenu:
                mainMenu.enabled = false;
                break;
            case SelectedMenu._levelSelection:
                levelSelection.enabled = false;                
                break;
            case SelectedMenu._carSelection:
                carSelection.enabled = false;
                EnableDisable_Garage(false);
                break;
            case SelectedMenu._loading:
                loading.enabled = false;                
                break;            
            default:
                break;
        }
    }
    public void Show_MainMenu()
    {
        OpenMenu(SelectedMenu._mainMenu);
    }
    public void Show_Levels()
    {
        OpenMenu(SelectedMenu._levelSelection);
    }
    public void Show_CarSelection()
    {
        OpenMenu(SelectedMenu._carSelection);      
    }
    public void Show_Loading(int buildIndex)
    {
        OpenMenu(SelectedMenu._loading);        
        levelLoader.LoadLevel(buildIndex);
    }   
    void EnableDisable_Garage(bool status)
    {
        //garage.enabled = status;
        cars.SetActive(status);
    }
    public void SetCoins()
    {
        txtCoins.text = levelsData.coins.ToString() + " $";
        levelSelectionScript.txtCoins.text = levelsData.coins.ToString() + " $";
        carSelectionScript.totalCoins.text = levelsData.coins.ToString() + " $";
    }

    #region Bottom Bar Events
    public void Play()
    {
        Show_Levels();
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.metaphasegames.megaramp.carracing.stuntgame.cargames.racinggames.huggywuggy.huggybuggy");
    }
    public void Settings()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Store()
    {

    }

    #endregion

}
