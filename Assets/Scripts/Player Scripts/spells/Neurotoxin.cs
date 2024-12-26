using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neurotoxin : MonoBehaviour, ISpellbase
{
    public void OnDisable() {
        EventManager.MagicEvent -= CastSpell;
    }
    public void Start()
    {
        EventManager.MagicEvent += CastSpell;
    }

    public void CastSpell() {
        Debug.Log("neurotoxin slash");
    }

}
