// Code auto-converted by Control Freak 2 on Tuesday, April 13, 2021!
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook1 : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool lockRotation=false;

    public float xRotation = 0f;
    void Start()
    {
        //ControlFreak2.CFCursor.lockState = CursorLockMode.Locked;        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = ControlFreak2.CF2Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = ControlFreak2.CF2Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        if(!lockRotation)
            playerBody.Rotate(0, mouseX, 0);
    }
}
