using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{
    public float health = 100;
    public Image healthBar;

    public AudioSource impact;
    public AudioClip impactClip;

    public bool die;
    public enum Character
    {
        player,
        enemy
    }
    public Character character;
    public ParticleSystem smokeParticle;
    public ParticleSystem blastEffect;

    [Header("Shield")]
    public GameObject shield;
    private bool shieldActivated = false;

    public int smokeEmition;

    public LevelManager levelManager;

    private void Awake()
    {
        //smokeParticle = GetComponent<ParticleSystem>();
    }
    public void Take_Damage(float damage, GameObject attacker)
    {
        if (shieldActivated)
            return;

        if (health <= 0.0f)
        {            
            return;
        }
     
        if (health - damage > 0.0f)
        {
            health -= damage;

            if (character == Character.player)
            {
                bl_DamageInfo info = new bl_DamageInfo((float)damage);
                info.Sender = attacker;
              
                bl_DamageDelegate.OnDamageEvent(info);

                attacker.SetIndicator();
            }
        }
        else
        {
            health = 0.0f;
        }

        if (healthBar)
            healthBar.fillAmount = ((health / 1) / 100);

        //Kill NPC
        if (health <= 0.0f)
        {
            Die();   
        }

        //Particle System
        if (health <= 60 && !smokeParticle.isPlaying)
        {
            smokeParticle.Play();
            Debug.Log("smoke emit");
        }
        
        if(smokeParticle.isPlaying)
        {
            var particleEmission = smokeParticle.emission;
            particleEmission.rateOverTime = particleEmission.rateOverTime.constant + smokeEmition;
        }
    }
    void Die()
    {
        die = true;

        if (character == Character.enemy)
        {
            GetComponent<RCC_AICarController>().enabled = false;
            GetComponent<Enemy_Weapon_Controller>().enabled = false;
            //Destroy(this.gameObject, 1);
            levelManager.Exclude_Enemy();
        }
        else if (character == Character.player)
        {        
            bl_DamageDelegate.OnDie();
        }
        
        GetComponent<RCC_CarControllerV3>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;      
      
        blastEffect.Play();
    }

    public void ActivateShield(bool activate)
    {
        shieldActivated = activate;
        StartCoroutine(StartTimer());      
    }
    IEnumerator StartTimer()
    {      
        yield return new WaitForSeconds(10f);
        shield.SetActive(false);
        shieldActivated = false;        
    }
}
