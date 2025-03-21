using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider damageBox;
    public float damage;
    public float tickTime;

    bool attacking;
    Animator anim;

    void Start() {
        damageBox.enabled = false;
        anim = transform.parent.GetComponent<Animator>();
    }

    void OnEnable()
    {
        PlayerInput.AttackEvent += Damage;
    }

    void OnDisable() {
        PlayerInput.AttackEvent -= Damage;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy") && other.gameObject.GetComponent<Enemy>() != null) {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void Damage() {
        anim.SetTrigger("attack");
        StartCoroutine(Damage_Enum());
    }

    IEnumerator Damage_Enum() {
        damageBox.enabled = true;
        yield return new WaitForSeconds(tickTime);
        damageBox.enabled = false;
    }
}
