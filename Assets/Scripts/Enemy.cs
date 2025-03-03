using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    public float damage;
    public UnityEvent<float> Event_Damage;

    bool damaging;

    // Start is called before the first frame update
    void Start()
    {
        if (Event_Damage == null) {
            Event_Damage = new UnityEvent<float>();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            damaging = true;
            StartCoroutine(DamageDelay(0.1f));
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            damaging = false;
        }
    }

    void OnDisable() {
        damaging = false;
        Debug.Log("enemy destroyed");
    }

    IEnumerator DamageDelay(float seconds) {
        yield return new WaitForSeconds(seconds);

        if (damaging) {
            Event_Damage?.Invoke(damage);
            StartCoroutine(DamageDelay(seconds));
        }
    }
}
