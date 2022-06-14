using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCollectable : MonoBehaviour
{
    public Image filledHealth;
    public Slider healthSlider;

    [SerializeField]
    private bl_HudDamageManager _bl_HudDamageManager;
    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            DamageManager damageManager = other.transform.root.GetComponent<DamageManager>();

            float health = damageManager.health;

            health += (100 - health);

            filledHealth.fillAmount = ((health / 1) / 100);
            //healthSlider.value = health;
            _bl_HudDamageManager.SetHealth(health);

            damageManager.health = health;

            damageManager.smokeParticle.Stop();
            var particleEmission = damageManager.smokeParticle.emission;
            particleEmission.rateOverTime = 5f;

            Destroy(this.gameObject);
        }
    }
}
