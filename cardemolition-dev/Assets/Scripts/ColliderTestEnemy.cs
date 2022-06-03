using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTestEnemy : MonoBehaviour
{
    float damageAmount = 1f;
    private void OnCollisionEnter(Collision collision)
    {       
        if (collision.collider.gameObject.tag == "Buffer")
        {
            TakeDamage(10);
        }
        else if (collision.collider.gameObject.tag == "Player" || collision.collider.gameObject.tag == "Enemy")
        {
            TakeDamage(6);
        }        
    }
    void TakeDamage(float damage)
    {
        damageAmount = damageAmount + damage;
        Debug.Log(transform.name + " : " + damageAmount);
    }
}
