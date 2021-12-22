using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Car : MonoBehaviour
{
    [SerializeField] private Transform t1, t2;
    public static float WheelTurnValue;

    private void Update()
    {
        GetCameraRotationDifference();
    }
    void GetCameraRotationDifference()
    {
        float dot = Vector3.Dot(t1.right, t2.position - t1.position);
        WheelTurnValue = dot;
    }
}
