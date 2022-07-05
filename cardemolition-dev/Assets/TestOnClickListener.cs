using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestOnClickListener : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;

    private void Start()
    {
        
    }

    public void SetListener()
    {
        button.onClick.RemoveAllListeners();        
        button.onClick.AddListener(OnClick);
    }
    public void SetListener2()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick2);
    }
    public void OnClick()
    {
        Debug.Log("On Click");
    }
    public void OnClick2()
    {
        Debug.Log("On Click 2");
    }
}
