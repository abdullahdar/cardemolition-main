using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public LevelsData levelsData;
    private MenuManager menuManager;
    public GameObject[] btnLevels;
    public Text[] levelTitle;

    [Header("TOP BAR")]
    public Text txtCoins;
    private void Awake()
    {
        menuManager = GetComponent<MenuManager>();
    }
    private void Start()
    {        
        menuManager.SetCoins();

        for (int i = 0; i < levelsData.levels.Count; i++)
        {
            btnLevels[i].GetComponent<Button>().interactable = !levelsData.IsLevelLocked(i);
            btnLevels[i].transform.GetChild(0).GetComponent<Image>().enabled = levelsData.IsLevelLocked(i);
            levelTitle[i].text = levelsData.levels[i].LevelDisplayName.ToUpper();
        }
    }
    public void OpenLevel(int levelNumber)
    {
        levelsData.levelSelected = levelNumber - 1;
        menuManager.Show_CarSelection();       
    }
    public void SelectLevel()
    {
        levelsData.levelSelected = levelsData.GetLastOpenedLevel();
        menuManager.Show_CarSelection();
    }
}
