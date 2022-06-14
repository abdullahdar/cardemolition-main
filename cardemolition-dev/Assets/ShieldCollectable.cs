using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollectable : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            DamageManager damageManager = other.transform.root.GetComponent<DamageManager>();

            damageManager.ActivateShield(true);

            damageManager.shield.SetActive(true);

            Destroy(this.gameObject);
        }
    }
}
