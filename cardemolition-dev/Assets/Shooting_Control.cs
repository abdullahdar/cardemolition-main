using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Control : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Transform start_RayPoint;
    public /*ParticleSystem*/GameObject muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    private Animator animator;
    public AudioSource playSound;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    public int counter = 0;
    public float tempTime = 0f;
    private void Start()
    {
        currentAmmo = maxAmmo;
    }    

    // Update is called once per frame
    void Update()
    {                
        if (ControlFreak2.CF2Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {            
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
    
    void Shoot()
    {        
        currentAmmo--;

        playSound.PlayOneShot(fireSound);
        muzzleFlash./*Play()*/SetActive(false);
        muzzleFlash./*Play()*/SetActive(true);

        RaycastHit hit;

        if (Physics.Raycast(start_RayPoint.position, start_RayPoint.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            DamageManager damage = hit.transform.GetComponent<DamageManager>();
            if (damage != null)
            {
                damage.Take_Damage(2f);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

        }
    }
}
