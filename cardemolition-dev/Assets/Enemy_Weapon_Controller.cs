using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        /*RaycastHit hit;
        if (Physics.Raycast(start_RayPoint.transform.position, start_RayPoint.transform.forward, out hit, range))
        {
            aim.transform.position = RCC_SceneManager.Instance.activeMainCamera.WorldToScreenPoint(hit.point);

            Debug.Log(hit.transform.name);

            if (ControlFreak2.CF2Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(hit);
            }

        }*/
    }
}
