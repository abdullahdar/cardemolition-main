using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("CANVASES")]
    [SerializeField]
    private Canvas gamePlayPanel;
    [SerializeField]
    private Canvas pausedPanel;
    [SerializeField]
    private Canvas completePanel;
    [SerializeField]
    private Canvas overPanel;
    [SerializeField]
    private Canvas rateUsAlert;

    [Header("ANIMATORS")]
    [SerializeField]
    private Animator pausedAnim;
    [SerializeField]
    private Animator completeAnim;
    [SerializeField]
    private Animator overAnim;
    public enum GameStatus
    {
        gamePlay,
        paused,
        gameOver,
        gameComplete
    }
    [Header("GAME STATUS")]
    public GameStatus gameStatus;

    [Header("RATE US THINGS")]
    public GameObject[] stars;
    public GameObject rateUsButton;
    private int rateUsStar;
    private bool canRate = true;

    private LevelsData levelsData;
    private CarData carData;

    private void Awake()
    {
        levelsData = GetComponent<LevelManager>().levelsData;
        carData = GetComponent<LevelManager>().carData;
    }

    #region Events
    public void Restart()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resume()
    {       
        ShowGamePlay();
    }
    public void Next()
    {
        if (levelsData.levels[levelsData.levelSelected].canRateUs && canRate)
        {
            canRate = false;
            Open_Rate_Us();
            return;
        } //<= RATE US

        if (levelsData.levelSelected < levelsData.levels.Count - 1)
        {            
            levelsData.levelSelected ++;
            
            if (AllRequirement_Completed())
                SceneManager.LoadScene(levelsData.GetCurrentSceneIndex());
            else
            {
                levelsData.openCarSelection = true;
                SceneManager.LoadScene(0);
            }
        }
        else
            SceneManager.LoadScene(0);
    }
    public void Home()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    bool AllRequirement_Completed()
    {
        bool levelRequirement = false;

        int selectedCar = carData.Selected_Car();
        int selectedGun = carData.Selected_Gun(selectedCar);
        int requiredCar = levelsData.GetRequiredCar(levelsData.levelSelected);
        int requiredGun = levelsData.GetRequiredGun(levelsData.levelSelected);

        if (selectedCar < requiredCar)
        {
            levelRequirement = false;            
        }
        else if (selectedCar == requiredCar && selectedGun < requiredGun)
        {            
            levelRequirement = false;
        }
        else
            levelRequirement = true;

        return levelRequirement;
    }

    #endregion

    #region Show Panels

    void OpenPanel(GameStatus status)
    {
        Disable_Panels();

        switch (status)
        {
            case GameStatus.gamePlay:
                if (Time.timeScale != 1)
                    Time.timeScale = 1;
                
                gamePlayPanel.enabled = true;
                gameStatus = status;
                break;
            case GameStatus.gameComplete:
                completePanel.enabled = true;
                gameStatus = status;
                break;
            case GameStatus.gameOver:
                overPanel.enabled = true;
                gameStatus = status;
                break;
            case GameStatus.paused:
                Time.timeScale = 0;
                pausedPanel.enabled = true;
                gameStatus = status;
                break;            
            default:
                break;
        }
    }
    void Disable_Panels()
    {
        switch (gameStatus)
        {
            case GameStatus.gamePlay:
                gamePlayPanel.enabled = false;
                break;
            case GameStatus.paused:
                pausedPanel.enabled = false;                
                break;
            case GameStatus.gameComplete:
                completePanel.enabled = false;                
                break;
            case GameStatus.gameOver:
                overPanel.enabled = false;                
                break;            
            default:
                break;
        }
    }
    public void ShowGamePlay()
    {        
        OpenPanel(GameStatus.gamePlay);
    }
    public void ShowPaused()
    {
        OpenPanel(GameStatus.paused);
    }
    public void ShowGameOver()
    {
        Invoke("GameOver", 2.0f);
    }
    public void ShowGameComplete()
    {
        Invoke("Complete", 2.0f);
    }
    public void Open_Rate_Us()
    {
        rateUsAlert.enabled = true;
        stars[4].GetComponent<Animation>().enabled = true;
    }
    void Complete()
    {        
        OpenPanel(GameStatus.gameComplete);
        levelsData.Unlock_NextLevel();
    }
    void GameOver()
    {
        OpenPanel(GameStatus.gameOver);
    }

    #endregion

    #region Rate Us

    public void RateStar(int starNumber)
    {
        rateUsStar = starNumber;

        if (rateUsStar < 5)
            stars[4].GetComponent<Animation>().enabled = false;
        else
            stars[4].GetComponent<Animation>().enabled = true;

        for (int i = 0; i < stars.Length; i++)
        {
            if (i < starNumber)
            {
                stars[i].transform.GetChild(0).transform.GetComponent<Image>().enabled = true;
                Debug.Log("Activated Star: " + i);
            }
            else
            {
                stars[i].transform.GetChild(0).transform.GetComponent<Image>().enabled = false;
                Debug.Log("DeActivated Star: " + i);
            }
        }
        rateUsButton.SetActive(true);
    }
    public void RateUs()
    {
        
        if (rateUsStar > 3)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.MetaphaseGames.PredatorHunting");
        }
        else
        {
            rateUsButton.SetActive(false);
        }
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].transform.GetChild(0).transform.GetComponent<Image>().enabled = true;
        }
        rateUsAlert.enabled = false;
        stars[4].GetComponent<Animation>().enabled = false;
        
    }

    #endregion

}
