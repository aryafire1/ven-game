using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject healthTextBox;

    Slider mask;
    TMP_Text healthText;
    HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        mask = healthBar.GetComponent<Slider>();
        healthManager = GetComponent<HealthManager>();
        
        mask.maxValue = healthManager.startingHealth;
        mask.value = mask.maxValue;

        healthText = healthTextBox.GetComponent<TMP_Text>();
        healthText.text = "Health: " + healthManager.startingHealth;
    }

    public void HealthUpdate(float health, float startingHealth) {
        mask.value = health/startingHealth * 100;
        healthText.text = "Health: " + health;

    }
}
