using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, ISpellbase
{
    public int healthToRegain;
    HealthManager playerHealth;
    EventManager eventManager;

    public void OnDisable() {
        EventManager.Healing -= CastSpell;
    }

    public void Start() {
        EventManager.Healing += CastSpell;
        playerHealth = transform.parent.gameObject.GetComponent<HealthManager>();
        eventManager = GetComponent<EventManager>();
    }

    public void CastSpell() {
        StartCoroutine(Regen(0.1f));
    }

    IEnumerator Regen(float seconds) {
        yield return new WaitForSeconds(seconds);
            
        if (playerHealth.health >= 100) {
            Debug.Log("can't heal past 100 bucko");
            //yield return null;
        }

        else {
            if (eventManager.CastCheck() > 0) {
                playerHealth.health += healthToRegain;
                Debug.Log($"health: {playerHealth.health}");
                StartCoroutine(Regen(seconds));
            }
        }
    }

    
}
