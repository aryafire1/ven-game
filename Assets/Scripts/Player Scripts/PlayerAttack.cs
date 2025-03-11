using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider damageBox;
    public float damage;

    bool attacking;

    void OnEnable()
    {
        EventManager.AttackEvent += Damage;
    }

    void OnDisable() {
        EventManager.AttackEvent -= Damage;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy") && other.gameObject.GetComponent<Enemy>() != null) {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void Damage() {
        transform.parent.GetComponent<Animator>().SetTrigger("attack");
    }
}
