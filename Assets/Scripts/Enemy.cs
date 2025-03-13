using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float health;
    float maxHealth;
    public float damage;
    public Slider slider;
    public UnityEvent<float> Event_Damage;
    Rigidbody rb;

    [HideInInspector]
    public bool damaging, poisoned;

    float poisonDamage;

    // Start is called before the first frame update
    public void Start()
    {
        if (Event_Damage == null) {
            Event_Damage = new UnityEvent<float>();
        }
        maxHealth = health;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void OnDisable() {
        damaging = false;
        //Debug.Log("enemy destroyed");
    }

    public void TakeDamage(float d) {
        if (!poisoned) {
            health = health - d;
            slider.value = health / maxHealth;
        }
        else {
            poisonDamage = poisonDamage + d;
        }

        if (health <= 0) {
            gameObject.SetActive(false);
        }
    }

    public void PoisonDamage() {
        poisoned = false;
        TakeDamage(poisonDamage * 2);
        poisonDamage = 0;
    }
}
