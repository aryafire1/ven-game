using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neurotoxin : MonoBehaviour, ISpellbase
{
    public GameObject player;

    [Range(1, 100)]
    public float magicRange;

    GameObject target;
    GameObject[] targets;

    public void OnDisable() {
        EventManager.MagicEvent -= CastSpell;
        Debug.Log("destroyed");
    }
    public void Start()
    {
        EventManager.MagicEvent += CastSpell;
        //rb = projectile.GetComponent<Rigidbody>();
    }

    public void CastSpell() {
        EventManager.MagicEvent -= CastSpell;

        targets = GameObject.FindGameObjectsWithTag("Enemy");
        target = targets[Random.Range(0, targets.Length)];

        StartCoroutine(Poison(5));
    }

    IEnumerator Poison(int seconds) {
        Debug.Log(seconds);
        seconds--;

        yield return new WaitForSeconds(1f);
        if (seconds < 0) {
            //end of countdown execution
            EventManager.MagicEvent += CastSpell;
            Destroy(target);
        }
        else if (seconds >= 0) {
            //loop until count ends
            StartCoroutine(Poison(seconds));
        }
    }


}
