using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider damageBox;
    public float damage;

    void OnEnable()
    {
        damageBox.enabled = false;
        EventManager.AttackEvent += EnableCollider;
    }

    void OnDisable() {
        EventManager.AttackEvent -= EnableCollider;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log("enemy damaged");
        }
    }

    void EnableCollider() {
        damageBox.enabled = true;
        damageBox.enabled = false;
    }
}
