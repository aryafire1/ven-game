using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    public float damage;
    public UnityEvent<float> Event_Damage;

    [HideInInspector]
    public bool damaging;

    // Start is called before the first frame update
    void Start()
    {
        if (Event_Damage == null) {
            Event_Damage = new UnityEvent<float>();
        }
    }

    void OnDisable() {
        damaging = false;
        //Debug.Log("enemy destroyed");
    }
}
