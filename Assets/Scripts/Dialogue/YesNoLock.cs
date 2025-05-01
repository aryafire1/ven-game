using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class YesNoLock : MonoBehaviour
{
    public DialogueInteract dialogue;

    void OnEnable() {
        StartCoroutine(Freeze(true));
    }
    void OnDisable() {
        StartCoroutine(Freeze(false));
    }

    IEnumerator Freeze(bool b) {
        yield return new WaitForSeconds(0.1f);
        PlayerInput.slowPlayer = b;
        PlayerInput.InteractEvent -= dialogue.StartText;
    }
}
