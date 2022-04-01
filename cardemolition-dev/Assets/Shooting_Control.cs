using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting_Control : MonoBehaviour
{
    // Start is called before the first frame update
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private float currentHitDistance;

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

    public Transform aim;
    private Image aimImage;

    public GameObject weapon;

    public int counter = 0;
    public float tempTime = 0f;

    /// <summary>
    /// BoxCast Things
    /// </summary>
    float m_MaxDistance = 2200f;
    bool m_HitDetect;

    float rotationX = 0;
    Vector3 localScale = new Vector3(0f, 80f, 0f);
    RaycastHit m_Hit;

    private void Start()
    {
        currentAmmo = maxAmmo;
        aimImage = aim.GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if (RCC_SceneManager.Instance.activeMainCamera)
        {
            ////BoxCast Attributes
            m_HitDetect = Physics.BoxCast(start_RayPoint.position, localScale / 2, start_RayPoint.forward, out m_Hit, start_RayPoint.rotation, m_MaxDistance);
           
            if (m_HitDetect)
            {
                //Output the name of the Collider your Box hit
                Debug.Log("Hit : " + m_Hit.collider.name);
                if (m_Hit.transform.tag == "Enemy")
                {
                    Debug.Log("Hit : " + m_Hit.collider.name);
                    Vector3 difference = m_Hit.transform.position - weapon.transform.position;
                    Quaternion rotation = Quaternion.LookRotation(difference, Vector3.up);

                    rotationX = rotation.eulerAngles.x;
                    aimImage.color = Color.red;
                }
                else
                {
                    rotationX = 0;
                    aimImage.color = Color.black;
                    Debug.Log("Hit : " + m_Hit.collider.name);
                }
            }
            else
            {
                rotationX = 0;
                aimImage.color = Color.black;
            }

            weapon.transform.rotation = Quaternion.Euler(rotationX, RCC_SceneManager.Instance.activeMainCamera.transform.eulerAngles.y, 0f);           
            
            RaycastHit hit;
            
            if (Physics.Raycast(start_RayPoint.transform.position, start_RayPoint.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.gameObject.name);
                Vector2 pos = RCC_SceneManager.Instance.activeMainCamera.WorldToScreenPoint(hit.point);
                aim.position = pos;                

                 if (ControlFreak2.CF2Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                 {
                     nextTimeToFire = Time.time + 1f / fireRate;
                     Shoot(hit);
                 }
            }          
        }
    }

    public void Missile()
    {

    }
    void Shoot(RaycastHit hit)
    {                
        playSound.PlayOneShot(fireSound);
        muzzleFlash.SetActive(false);
        muzzleFlash.SetActive(true);
                
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(start_RayPoint.position, start_RayPoint.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(start_RayPoint.position + start_RayPoint.forward * m_Hit.distance, localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(start_RayPoint.position, start_RayPoint.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(start_RayPoint.position + start_RayPoint.forward * m_MaxDistance, transform.localScale);
        }
    }
}
