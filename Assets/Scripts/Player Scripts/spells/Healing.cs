using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour, ISpellbase
{
    public GameObject player;

    bool isCrouching;


    public void OnDisable() {
        EventManager.Healing -= CastSpell;
        //EventManager.LookDown -= SwitchBool;
    }

    public void Start() {
        EventManager.Healing += CastSpell;
        //EventManager.LookDown += SwitchBool;
    }

    public void CastSpell() {
        Debug.Log("healing time wow");
    }

    /*void SwitchBool() {
        isCrouching = !isCrouching;
    }*/
}
