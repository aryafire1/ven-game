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
        if (startingHealth == 0) {
            health = 100;
        }
        else {
            health = startingHealth;
        }

        hud = GetComponent<HUDManager>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            health = health - 20;
            hud.HealthUpdate();
            //Debug.Log(health);
        }
    }

    void Death() {
        if (health <= 0) {
            this.gameObject.SetActive(false);
            Debug.Log("you died idiot");
        }
    }
}
