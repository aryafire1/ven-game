using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTest : MonoBehaviour
{
    OldInput i;
    int tempHealth;

    // Start is called before the first frame update
    void Start()
    {
        i = GetComponent<OldInput>();
        tempHealth = 100;

        StartCoroutine(Heal());
    }

    // Update is called once per frame
    void Update()
    {
        DevDamage();
    }

    void DevDamage() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            tempHealth = tempHealth - 20;
        }
    }

    IEnumerator Heal() {
        yield return new WaitForSeconds(.5f);

        if (i.crouching == true && Input.GetKey(KeyCode.Mouse1)) {
            if (tempHealth < 100) {
                tempHealth ++;
                Debug.Log(tempHealth);
            }
        }

        StartCoroutine(Heal());
    }
}
