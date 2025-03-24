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
    Renderer renderer;

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
        renderer = gameObject.GetComponent<Renderer>();
    }

    void OnDisable() {
        damaging = false;
        //Debug.Log("enemy destroyed");
    }

    public void TakeDamage(float d) {
        if (!poisoned) {
            health = health - d;
            slider.value = health / maxHealth;
            StartCoroutine(DamageFlash());
        }
        else {
            poisonDamage = poisonDamage + d;
        }

        if (health <= 0) {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void PoisonDamage() {
        poisoned = false;
        TakeDamage(poisonDamage * 2);
        poisonDamage = 0;
    }

    IEnumerator DamageFlash() {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.material.color = Color.white;
    }
}
