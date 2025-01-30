using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject healthBar;

    Slider mask;
    HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        mask = healthBar.transform.GetChild(0).gameObject.GetComponent<Slider>();
        healthManager = GetComponent<HealthManager>();
        
        mask.maxValue = healthManager.startingHealth; //you are the problem
        mask.value = mask.maxValue;
    }

    public void HealthUpdate() {
        mask.value = (healthManager.health/healthManager.startingHealth) * 100;
    }
}
