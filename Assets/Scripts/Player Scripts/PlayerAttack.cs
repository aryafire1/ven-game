using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider damageBox;
    public float damage;
    public float tickTime;

    bool attacking;

    void Start() {
        damageBox.enabled = false;
    }

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
        StartCoroutine(Damage_Enum());
    }

    IEnumerator Damage_Enum() {
        damageBox.enabled = true;
        yield return new WaitForSeconds(tickTime);
        damageBox.enabled = false;
    }
}
