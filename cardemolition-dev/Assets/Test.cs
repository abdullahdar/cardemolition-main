using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MonoBehaviour[] scripts;

    private void Start()
    {
        foreach (MonoBehaviour script in scripts)
            script.enabled = false;
    }

}
