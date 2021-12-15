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

    public GameObject aim;

    public GameObject weapon;

    public int counter = 0;
    public float tempTime = 0f;
    private void Start()
    {
        currentAmmo = maxAmmo;
    }    

    private void FixedUpdate()
    {
        if (RCC_SceneManager.Instance.activeMainCamera)
        {
            weapon.transform.eulerAngles = new Vector3(0f, RCC_SceneManager.Instance.activeMainCamera.transform.eulerAngles.y, 0f);
            RaycastHit hit;
            if (Physics.Raycast(start_RayPoint.transform.position, start_RayPoint.transform.forward, out hit, range))
            {
                aim.transform.position = RCC_SceneManager.Instance.activeMainCamera.WorldToScreenPoint(hit.point);                

                if (ControlFreak2.CF2Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot(hit);
                }
            }
        }
    }


    void Shoot(RaycastHit hit)
    {                
        playSound.PlayOneShot(fireSound);
        muzzleFlash.SetActive(false);
        muzzleFlash.SetActive(true);
        
        Debug.Log(hit.transform.name);
        DamageManager damageManager = hit.transform.GetComponent<DamageManager>();
        
        if (damageManager != null)
        {
            damageManager.Take_Damage(damage);
        }
        
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
        
        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
