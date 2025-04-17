using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class YesNoLock : MonoBehaviour
{
    void OnEnable() {
        PlayerInput.slowPlayer = true;
    }
    void OnDisable() {
        PlayerInput.slowPlayer = false;
    }
}
