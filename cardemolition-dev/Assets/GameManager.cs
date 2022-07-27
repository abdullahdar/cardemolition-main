using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

}
