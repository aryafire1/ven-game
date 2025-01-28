using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class EventManager : MonoBehaviour
{
    InputSystemActions inputActions;
    InputAction dash, magic, look, attack;

    //input actions
    public static event Action DashEvent, LookUp, LookDown, AttackEvent;

    //combo actions
    public static event Action Healing, Poison;

    public static bool slowPlayer;
    public static float casting;
    public static float looking;


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
    void Update() {
        looking = look.ReadValue<float>();
        casting = magic.ReadValue<float>();
    }


    //input voids
    void Dash(InputAction.CallbackContext context) {
        //Debug.Log("dash");
        DashEvent?.Invoke();
    }
    void Magic(InputAction.CallbackContext context) {
        Debug.Log("magic call");
        MagicCall();
    }
    void Look(InputAction.CallbackContext context) {
        if (looking > 0) {
            Debug.Log("look up");
            LookUp?.Invoke();
        }
        if (looking < 0) {
            Debug.Log("crouch");
            LookDown?.Invoke();
        }
        if (looking == 0) {
            slowPlayer = false;
        }
    }
    void Attack(InputAction.CallbackContext context) {
        Debug.Log("slash");
        AttackEvent?.Invoke();
    }


    void MagicCall() {        
        if (looking < 0 && casting > 0) {
            Healing?.Invoke();
            slowPlayer = true;
        }
        else {
            Poison?.Invoke();
        }
    }


}
