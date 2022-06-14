using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollectable : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Shooting_Control shooting_Control = other.transform.root.GetComponent<Shooting_Control>();
            //shooting_Control.Set_NextTimeToMissile();
            if (shooting_Control != null)
                shooting_Control.Set_NextTimeToMissile();

            Destroy(this.gameObject);
        }
    }
}
