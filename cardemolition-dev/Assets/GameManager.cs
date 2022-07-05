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

    private void Awake()
    {
        levelsData = GetComponent<LevelManager>().levelsData;
    }

    #region Events
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resume()
    {       
        ShowGamePlay();
    }
    public void Next()
    {
        SceneManager.LoadScene(0);
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
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
        OpenPanel(GameStatus.gameOver);
    }
    public void ShowGameComplete()
    {
        OpenPanel(GameStatus.gameComplete);
        levelsData.Unlock_NextLevel();
    }

    #endregion

}
