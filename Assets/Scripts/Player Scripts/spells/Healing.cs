using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, ISpellbase
{
    public GameObject player;

    public void OnDisable() {
        EventManager.Healing -= CastSpell;
    }

    public void Start() {
        EventManager.Healing += CastSpell;
    }

    public void CastSpell() {
        StartCoroutine(Regen(1));
    }

    IEnumerator Regen(int seconds) {
        //replace this once you set up the health system kk
        Debug.Log("player health regen variable goes here");

        yield return new WaitForSeconds(seconds);

        if (EventManager.inputActions.Player.Magic.ReadValue<float>() > 0) {
            StartCoroutine(Regen(seconds));
        }
    }

    
}
