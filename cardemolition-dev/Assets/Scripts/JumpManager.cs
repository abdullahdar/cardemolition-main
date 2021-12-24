using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpManager : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidbody;
    void Start()
    {
        rigidbody=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ControlFreak2.CF2Input.GetButton("Jump"))
        {
            Twist();
        }
    }

    public void Twist()
    {
        Debug.Log("Twisted");
        rigidbody.AddForce(Vector3.up*1000,ForceMode.Impulse);
    }
}
