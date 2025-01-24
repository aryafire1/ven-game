using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        if (health == 0) {
            health = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            health = health - 20;
            Debug.Log(health);
        }
    }

    void Death() {
        if (health <= 0) {
            this.gameObject.SetActive(false);
            Debug.Log("you died idiot");
        }
    }
}
