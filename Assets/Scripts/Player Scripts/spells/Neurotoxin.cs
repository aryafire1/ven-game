using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neurotoxin : Spellbase
{
    [Header("Poison Stuff")]
    [Range(1, 10)]
    public int poisonRange;
    [Tooltip("For edit view only")]
    public bool showGizmo;
    [Space(10)]

    GameObject target;
    Enemy targetScript;
    GameObject[] targets;

    public override void OnDisable() {
        PlayerInput.Poison -= CastSpell;
    }
    public override void Start()
    {
        PlayerInput.Poison += CastSpell;
        //rb = projectile.GetComponent<Rigidbody>();
    }

    public override void CastSpell() {
        //tells coroutine to start
        bool coStart = true;

        //this disables ability to cast again
        PlayerInput.Poison -= CastSpell;

        //checks if i already haven't assigned the target so it's not searching through every object
        if (target == null) {
            targets = GameObject.FindGameObjectsWithTag("Enemy");

            float shortestDist = poisonRange;
            for (int i = 0; i < targets.Length; i++) {
                float enemyDistance = Vector3.Distance(transform.parent.position, targets[i].transform.position);
                if (enemyDistance < shortestDist) {
                    shortestDist = enemyDistance;
                    target = targets[i];
                    targetScript = target.GetComponent<Enemy>();
                }
            }
        }

        //if it's still null after all that then no spell for u
        if (target == null) {
            Debug.Log("no more enemies");
            //enables casting
            PlayerInput.Poison += CastSpell;
            coStart = false;
        }
        
        if (coStart == true) {
            TempColorChange(target);
            targetScript.poisoned = true;
            StartCoroutine(Poison(5));
        }
    }

    IEnumerator Poison(int seconds) {
        Debug.Log(seconds);
        seconds--;

        yield return new WaitForSeconds(1f);
        if (seconds < 0) {
            //end of countdown execution
            //enables casting
            PlayerInput.Poison += CastSpell;
            targetScript.PoisonDamage();
            targetScript.poisoned = false;
        }
        else if (seconds >= 0) {
            //loop until count ends
            StartCoroutine(Poison(seconds));
        }
    }

    void OnDrawGizmos() {
        if (showGizmo == true) {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.parent.position, poisonRange);
        }
    }

    void TempColorChange(GameObject _target) {
        _target.GetComponent<Renderer>().material.color = Color.red;
    }


}
