using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public LevelsData levelsData;
    private MenuManager menuManager;
    public GameObject[] btnLevels;

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
        }
    }
    public void OpenLevel(int levelNumber)
    {
        levelsData.levelSelected = levelNumber - 1;
        menuManager.Show_CarSelection();       
    }
}
