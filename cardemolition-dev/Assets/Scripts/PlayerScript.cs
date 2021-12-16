using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int health = 100;
    public ParticleSystem playerParticelSystem;

    private void Awake()
    {
        playerParticelSystem = GetComponent<ParticleSystem>();
    }

    public void DamageManager(int damage)
    {
        health = health - damage;

        if(health<80 && !playerParticelSystem.isPlaying)
            playerParticelSystem.Play();


    }
}
