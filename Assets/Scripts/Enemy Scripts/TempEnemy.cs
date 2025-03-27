using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : Enemy
{
    public float speed;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
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

    IEnumerator DamageDelay(float seconds) {
        yield return new WaitForSeconds(seconds);

        if (damaging) {
            Event_Damage?.Invoke(damage);
            StartCoroutine(DamageDelay(seconds));
        }
    }
}
