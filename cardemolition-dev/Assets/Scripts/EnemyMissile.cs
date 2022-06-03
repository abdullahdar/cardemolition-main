using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    public float speed;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<DamageManager>().Take_Damage(10);
        }

        Destroy(gameObject);
    }

}
