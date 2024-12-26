using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class EventManager : MonoBehaviour
{
    public InputSystemActions inputActions;
    InputAction dash;
    InputAction magic;


    public static event Action DashEvent;
    public static event Action MagicEvent;


    void Awake() {
        inputActions = new InputSystemActions();
    }
    void OnEnable() {
        dash = inputActions.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        magic = inputActions.Player.Magic;
        magic.Enable();
        magic.performed += Magic;
    }
    void OnDisable() {
        dash.Disable();
        magic.Disable();
    }


    void Dash(InputAction.CallbackContext context) {
        //Debug.Log("dash");
        DashEvent?.Invoke();
    }
    void Magic(InputAction.CallbackContext context) {
        Debug.Log("magic call");
        MagicEvent?.Invoke();
    }

}
