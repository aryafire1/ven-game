using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float startingHealth;

    [HideInInspector]
    public float health;

    HUDManager hud;
    bool hurting;

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

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            StartCoroutine(Damage(1));
            //Debug.Log(health);
            hurting = true;

            DeathCheck();
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy")) {
            hurting = false;
        }
    }

    void DeathCheck() {
        if (health <= 0) {
            this.gameObject.SetActive(false);
            Debug.Log("you died idiot");
        }
    }

    IEnumerator Damage(float damage) {
        yield return new WaitForSeconds(0.1f);
        health = health - damage;

        if (hurting == true) {
            StartCoroutine(Damage(damage));
        }
    }
}
