using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform joystickValue;

    // Update is called once per frame
    void Update()
    {
        float horizontal = ControlFreak2.CF2Input.GetAxis("Horizontal");
        float vertical = ControlFreak2.CF2Input.GetAxis("Vertical");

        //joystickValue.transform.eulerAngles = new Vector3(joystickValue.transform.eulerAngles.x, joystickValue.transform.eulerAngles.y, cam.eulerAngles.y);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg/* + cam.eulerAngles.y*/;
            
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Debug.Log(angle+" target angle: "+targetAngle);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
