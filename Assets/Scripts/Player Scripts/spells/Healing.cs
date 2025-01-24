using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, ISpellbase
{
    public GameObject player;
    float playerHealth;

    public void OnDisable() {
        EventManager.Healing -= CastSpell;
    }

    public void Start() {
        EventManager.Healing += CastSpell;
        playerHealth = player.GetComponent<HealthManager>().health;
    }

    public void CastSpell() {
        StartCoroutine(Regen(0.1f));
    }

    IEnumerator Regen(float seconds) {
            
        if (playerHealth == 100) {
            Debug.Log("can't heal past 100 bucko");
            yield return null;
        }

        else {
            if (EventManager.casting > 0) {
                StartCoroutine(Regen(0.1f));
            }
            yield return new WaitForSeconds(seconds);
        }
    }

    
}
