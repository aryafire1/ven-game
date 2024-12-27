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
    InputAction look;
    InputAction attack;


    public static event Action DashEvent;
    public static event Action MagicEvent;
    public static event Action LookUp;
    public static event Action LookDown;
    public static event Action AttackEvent;


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

        look = inputActions.Player.Look;
        look.Enable();
        look.performed += Look;

        attack = inputActions.Player.Attack;
        attack.Enable();
        attack.performed += Attack;
    }
    void OnDisable() {
        dash.Disable();
        magic.Disable();
        look.Disable();
        attack.Disable();
    }


    void Dash(InputAction.CallbackContext context) {
        //Debug.Log("dash");
        DashEvent?.Invoke();
    }
    void Magic(InputAction.CallbackContext context) {
        Debug.Log("magic call");
        MagicEvent?.Invoke();
    }

        void Look(InputAction.CallbackContext context) {
        float looking = look.ReadValue<float>();
        if (looking > 0) {
            Debug.Log("look up");
            LookUp?.Invoke();
        }
        if (looking < 0) {
            Debug.Log("crouch");
            LookDown?.Invoke();
        }
    }

    void Attack(InputAction.CallbackContext context) {
        Debug.Log("slash");
        AttackEvent?.Invoke();
    }

}
