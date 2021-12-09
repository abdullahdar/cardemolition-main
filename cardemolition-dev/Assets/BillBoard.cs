using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform cam;

    public void Start()
    {
        cam = Camera.main.transform;
    }

    public void LateUpdate()
    {
        try
        {
            transform.LookAt(transform.position + cam.forward);

        }
        catch (System.Exception)
        {

           
        }
    }
}
