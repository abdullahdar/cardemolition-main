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
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    [SerializeField]
    private TrailRenderer BulletTrail;

    private float nextTimeToFire = 0f;
    private Animator animator;
    public AudioSource playSound;
    public AudioClip fireSound;
    public AudioClip reloadSound;

    public Transform aim;
    private Image aimImage;
    private bool focusOnEnemy = false;

    public GameObject weapon;

    public int counter = 0;
    public float tempTime = 0f;

    /// <summary>
    /// BoxCast Things
    /// </summary>
    float m_MaxDistance = 2200f;
    bool m_HitDetect;

    float rotationX = 0;
    float rotationY = 0;
    Vector3 localScale = new Vector3(0f, 80f, 0f);
    RaycastHit m_Hit;
    private Transform enemyTransform;    

    [Header("MISSILE")]
    public GameObject missilePrefab;
    public Transform missileSpawnPoint;
    private float nextTimeToMissile;
    public float missileFireRate = 1f;
    public Image missileImage;

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
                if (m_Hit.transform.tag == "Enemy")
                {                    
                    aimImage.color = Color.red;
                    focusOnEnemy = true;

                    enemyTransform = m_Hit.transform;
                }
                else
                {                    
                    aimImage.color = Color.black;                    
                }
            }
            else
            {       
                aimImage.color = Color.black;
            }

            if(focusOnEnemy && enemyTransform)
            {             
                Vector3 right = RCC_SceneManager.Instance.activeMainCamera.transform.TransformDirection(Vector3.right);
                Vector3 toOther = enemyTransform.position - RCC_SceneManager.Instance.activeMainCamera.transform.position;
                float dot = Vector3.Dot(right, toOther);
                

                if(dot < 20f && dot > -20f)
                {
                    Vector3 difference = enemyTransform.position - weapon.transform.position;
                    Quaternion rotation = Quaternion.LookRotation(difference, Vector3.up);
                    weapon.transform.rotation = rotation;                    
                }
                else
                {
                    focusOnEnemy = false;
                    weapon.transform.rotation = Quaternion.Euler(0, RCC_SceneManager.Instance.activeMainCamera.transform.eulerAngles.y, 0f);                    
                }
            }
            else
            {                
                weapon.transform.rotation = Quaternion.Euler(0, RCC_SceneManager.Instance.activeMainCamera.transform.eulerAngles.y, 0f);                
            }            
            
            RaycastHit hit;
            
            if (Physics.Raycast(start_RayPoint.transform.position, start_RayPoint.transform.forward, out hit, range))
            {                
                Vector2 pos = RCC_SceneManager.Instance.activeMainCamera.WorldToScreenPoint(hit.point);
                aim.position = pos;                

                 if (ControlFreak2.CF2Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                 {
                     nextTimeToFire = Time.time + 1f / fireRate;
                     Shoot(hit);
                 }         
                if (ControlFreak2.CF2Input.GetButton("Fire2") && Time.time >= nextTimeToMissile)
                {
                    Debug.Log("Missile");
                    nextTimeToMissile = Time.time + 1f / missileFireRate;                    
                    Missile(hit);
                }
                else
                {                    
                    missileImage.fillAmount = 1-((nextTimeToMissile - Time.time) / 4 );
                }
            }
            
        }
    }
    void Missile(RaycastHit hit)
    {
        DamageManager damageManager = hit.transform.GetComponent<DamageManager>();

        if (damageManager != null)
        {
            missilePrefab.GetComponent<Tarodev.Missile>().enemyNotFound = false;
            missilePrefab.GetComponent<Tarodev.Missile>()._target = hit.transform.position;           
            missilePrefab.GetComponent<Tarodev.Missile>().targetRigidBody = hit.transform.GetComponent<Rigidbody>();
        }
        else
        {
            missilePrefab.GetComponent<Tarodev.Missile>().enemyNotFound = true;
            missilePrefab.GetComponent<Tarodev.Missile>()._target = hit.point;            
        }


        GameObject missile = Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);       

    }
    void Shoot(RaycastHit hit)
    {
        TrailRenderer trail = Instantiate(BulletTrail, start_RayPoint.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, hit));

        playSound.PlayOneShot(fireSound);
       
        muzzleFlash.Play();
                
        DamageManager damageManager = hit.transform.GetComponent<DamageManager>();
        
        if (damageManager != null)
        {
            damageManager.Take_Damage(damage,this.gameObject);
        }
        
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }                                  
    }

    public void Set_NextTimeToMissile()
    {
        nextTimeToMissile = Time.time;
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

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        //Animator.SetBool("IsShooting", false);
        Trail.transform.position = Hit.point;
       
        Instantiate(impactEffect, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }
}
