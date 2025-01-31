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
        mask = healthBar.GetComponent<Slider>();
        healthManager = GetComponent<HealthManager>();
        
        mask.maxValue = healthManager.startingHealth;
        mask.value = mask.maxValue;
    }

    public void HealthUpdate(float health, float startingHealth) {
        mask.value = health/startingHealth * 100;
    }
}
