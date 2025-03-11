using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    public float health;
    public float damage;
    public UnityEvent<float> Event_Damage;

    [HideInInspector]
    public bool damaging, poisoned;

    float poisonDamage;

    // Start is called before the first frame update
    void Start()
    {
        if (Event_Damage == null) {
            Event_Damage = new UnityEvent<float>();
        }
    }

    void OnDisable() {
        damaging = false;
        //Debug.Log("enemy destroyed");
    }

    public void TakeDamage(float d) {
        if (!poisoned) {
            health = health - d;
            //knockback goes here
        }
        else {
            Debug.Log($"poison damage: {poisonDamage}");
            poisonDamage = poisonDamage + d;
        }

        if (health <= 0) {
            gameObject.SetActive(false);
        }
    }

    public void PoisonDamage() {
        health = health - (poisonDamage * 2);
        poisonDamage = 0;
    }
}
