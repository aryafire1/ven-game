using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, ISpellbase
{
    public int healthToRegain;
    HealthManager playerHealth;
    PlayerInput eventManager;
    HUDManager hud;

    public void OnDisable() {
        PlayerInput.Healing -= CastSpell;
    }

    public void Start() {
        PlayerInput.Healing += CastSpell;
        playerHealth = transform.parent.gameObject.GetComponent<HealthManager>();
        eventManager = transform.parent.gameObject.GetComponent<PlayerInput>();
        hud = transform.parent.gameObject.GetComponent<HUDManager>();
    }

    public void CastSpell() {
        Debug.Log("Healing time");
        StartCoroutine(Regen(0.1f));
    }

    IEnumerator Regen(float seconds) {
        yield return new WaitForSeconds(seconds);
            
        if (playerHealth.health >= 100) {
            Debug.Log("can't heal past 100 bucko");
            //yield return null;
        }

        else if (eventManager.CastCheck() > 0) {
                playerHealth.health += healthToRegain;
                //hud.HealthUpdate(playerHealth.health, playerHealth.startingHealth);
                //Debug.Log($"health: {playerHealth.health}");
                StartCoroutine(Regen(seconds));
            }
    }

    
}
