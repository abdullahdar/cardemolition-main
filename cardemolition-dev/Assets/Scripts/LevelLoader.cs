using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Image loadingBar;

    public void LoadLevel(int buildIndex)
    {
        StartCoroutine(LoadSynchronously(buildIndex));       
    }
    IEnumerator LoadSynchronously(int buildIndex)
    {                          
            var operation = SceneManager.LoadSceneAsync(buildIndex);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                loadingBar.fillAmount = progress;                
                yield return null;
            }                   
    }
}
