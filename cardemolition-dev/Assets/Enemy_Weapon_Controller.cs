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
    
    public Transform weapon;

    bool hitDetect;
    bool hitDistance;
    RaycastHit hit;

    [Header("MISSILE")]
    public GameObject missilePrefab;
    public Transform missileSpawnPoint;
    private float nextTimeToMissile;
    public float missileFireRate = 1f;
    float time = 0;

    private RCC_AICarController rCC_AICarController;

    private void Awake()
    {
        rCC_AICarController = GetComponent<RCC_AICarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rCC_AICarController.targetChase)
        {
            time += Time.deltaTime;
            weapon.LookAt(rCC_AICarController.targetChase);

            if(time > 5f)
                Shoot_Missile();
            else
            {
                if (Physics.Raycast(start_RayPoint.transform.position, start_RayPoint.transform.forward, out hit, range))
                {
                  if (Time.time >= nextTimeToFire)
                  {
                      nextTimeToFire = Time.time + 1f / fireRate;
                      Shoot(hit);
                  }
                }            
            }
        }
        else
        {
            time = 0;
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

    void Shoot_Missile()
    {
        if (Time.time >= nextTimeToMissile)
        {
            nextTimeToMissile = Time.time + 1f / missileFireRate;
            GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
        }
    }
}
