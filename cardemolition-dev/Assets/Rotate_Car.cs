using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Car : MonoBehaviour
{
    [SerializeField] private Transform car, camera;
    public static float WheelTurnValue;

    public static float magnitude;

    public static float verticalValue;

    public GameObject test;
    public Transform joyStick;

    private void Update()
    {
        GetCarRotationAngle();
    }
    void GetCameraRotationDifference()
    {
        float dot = Vector3.Dot(car.right, camera.position - car.position);
        WheelTurnValue = dot;        
    }

    void GetCarRotationAngle()
    {                
        joyStick.rotation = Quaternion.Euler(0,0,camera.eulerAngles.y);

        float horizontal = ControlFreak2.CF2Input.GetAxis("Horizontal");
        float vertical = ControlFreak2.CF2Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        magnitude = direction.magnitude;

        if (magnitude >= 0.1f)
        {            
            float dot = Vector3.Dot(car.right, direction);
            WheelTurnValue = dot;

            float throttleDot = Vector3.Dot(car.forward, direction);
            
            if (throttleDot > -0.8f)
                verticalValue = 1;
            else if (throttleDot < -0.8f)
                verticalValue = -1;
            else
                verticalValue = 0;                      
        }
        else
        {
            WheelTurnValue = 0;
            verticalValue = 0;
        }        
    }
}
