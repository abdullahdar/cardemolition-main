using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneManager : MonoBehaviour
{

    private void Start()
    {
        
    }
    public void Environment1()
    {
        SceneManager.LoadScene(1);
    }
    public void Environment2()
    {
        SceneManager.LoadScene(2);
    }
    public void Environment3()
    {
        SceneManager.LoadScene(3);
    }
}
