using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neurotoxin : MonoBehaviour, ISpellbase
{
    public GameObject player;

    [Header("Poison Stuff")]
    [Range(1, 10)]
    public int poisonRange;
    [Tooltip("For edit view only")]
    public bool showGizmo;
    [Space(10)]

    GameObject target;
    GameObject[] targets;

    public void OnDisable() {
        EventManager.Poison -= CastSpell;
        Debug.Log("destroyed");
    }
    public void Start()
    {
        EventManager.Poison += CastSpell;
        //rb = projectile.GetComponent<Rigidbody>();
    }

    public void CastSpell() {
        //tells coroutine to start
        bool coStart = true;

        //this disables ability to cast again
        EventManager.Poison -= CastSpell;

        targets = GameObject.FindGameObjectsWithTag("Enemy");

        //old code that grabbed random enemy
        //target = targets[Random.Range(0, targets.Length)];

        float shortestDist = poisonRange;
        for (int i = 0; i < targets.Length; i++) {
            float enemyDistance = Vector3.Distance(player.transform.position, targets[i].transform.position);
            if (enemyDistance < shortestDist) {
                shortestDist = enemyDistance;
                target = targets[i];
            }
        }
        TempColorChange(target);

        if (target == null) {
            Debug.Log("no more enemies");
            //enables casting
            EventManager.Poison += CastSpell;
            coStart = false;
        }
        
        if (coStart == true) {
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
            EventManager.Poison += CastSpell;
            target.SetActive(false);
        }
        else if (seconds >= 0) {
            //loop until count ends
            StartCoroutine(Poison(seconds));
        }
    }

    void OnDrawGizmos() {
        if (showGizmo == true) {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, poisonRange);
        }
    }

    void TempColorChange(GameObject _target) {
        _target = target;
        _target.GetComponent<Renderer>().material.color = Color.red;
    }


}
