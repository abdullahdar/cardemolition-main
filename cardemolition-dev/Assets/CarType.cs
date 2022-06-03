using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarType : MonoBehaviour
{
    public enum Car_Type
    {
        _4X,
        aX_car1,
        b_car_1,
        buggy,
        jip,
        tos
    }
    public Car_Type carType;

    public MeshRenderer[] barriers;
    public MeshRenderer[] guns;
    public MeshRenderer[] missiles;
    public void ActivateBarrier(int barrierNumber)
    {
        if (carType == Car_Type.buggy) //< DONT HAVE ANY BARRIER IN THIS CAR
            return;

        barriers[barrierNumber].enabled = true;
        barriers[barrierNumber].gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    public void ActivateEnemyBarrier(int barrierNumber,bool hasBarrier)
    {
        barriers[barrierNumber].enabled = hasBarrier;
        barriers[barrierNumber].gameObject.GetComponent<BoxCollider>().enabled = hasBarrier;
    }
    public void ActivateGun(int gunNumber)
    {
        guns[gunNumber].enabled = true;
    }
    public void ActivateEnemyGun(int gunNumber,bool hasGun)
    {
        guns[gunNumber].enabled = hasGun;
        GetComponent<Enemy_Weapon_Controller>().enabled = hasGun;
    }
    public void ActivateMissile(int missileNumber)
    {
        missiles[missileNumber].enabled = true;
    }
    public void Set_Position(Vector3 postion)
    {
        transform.position = postion;
    }
    public void Set_Rotation(Vector3 rotation)
    {
        transform.eulerAngles = rotation;
    }
    public void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }
}
