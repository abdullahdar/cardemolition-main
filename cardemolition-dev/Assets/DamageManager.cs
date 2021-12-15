using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{
    public float health = 100;
    public Canvas healthCanvas;
    public Image healthBar;

    public AudioSource impact;
    public AudioClip impactClip;

    ParticleSystem playerParticelSystem;
    public int smokeEmition;

    private void Awake()
    {
        playerParticelSystem = GetComponent<ParticleSystem>();
    }
    public void Take_Damage(float damage)
    {
        if (health <= 0.0f)
        {            
            return;
        }

        if (health - damage > 0.0f)
        {
            health -= damage;
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
        if (health <= 60 && !playerParticelSystem.isPlaying)
            playerParticelSystem.Play();
        
        if(playerParticelSystem.isPlaying)
        {
            var particleEmission = playerParticelSystem.emission;
            particleEmission.rateOverTime = particleEmission.rateOverTime.constant + smokeEmition;
        }
    }

    void Die()
    {        
        Destroy(this.gameObject,1);
    }
}
