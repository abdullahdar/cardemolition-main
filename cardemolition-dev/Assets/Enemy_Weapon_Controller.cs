using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public Transform start_RayPoint;
    public GameObject muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    
    public AudioSource playSound;
    public AudioClip fireSound;

    public Transform player;
    public Transform weapon;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weapon.LookAt(player);

        RaycastHit hit;
        if (Physics.Raycast(start_RayPoint.transform.position, start_RayPoint.transform.forward, out hit, range))
        {            

            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(hit);
            }

        }
    }
    void Shoot(RaycastHit hit)
    {        
        DamageManager damageManager = hit.transform.GetComponent<DamageManager>();


        if (damageManager != null && !damageManager.die)
        {
            Debug.Log("Muzzle"+hit.transform.gameObject.name);

            playSound.PlayOneShot(fireSound);
            muzzleFlash.SetActive(false);
            muzzleFlash.SetActive(true);

            damageManager.Take_Damage(damage);            

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal),damageManager.transform);
            Destroy(impactGO, 2f);
        }
    }
}
