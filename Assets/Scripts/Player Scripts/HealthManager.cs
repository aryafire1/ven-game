using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float startingHealth;

    [HideInInspector]
    public float health;

    HUDManager hud;

    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponent<HUDManager>();

        if (startingHealth == 0) {
            startingHealth = 100;
        }
            health = startingHealth;
    }

    void Update() {
        hud.HealthUpdate(health, startingHealth);
    }

    public void Damage(float damage) {
        health = health - damage;
    }

    void DeathCheck() {
        if (health <= 0) {
            this.gameObject.SetActive(false);
            Debug.Log("you died idiot");
        }
    }
}
