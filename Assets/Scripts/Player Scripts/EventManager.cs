using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class EventManager : MonoBehaviour
{
#region Variables

    InputSystemActions inputActions;
    InputAction dash, magic, look, attack;

    //input actions
    public static event Action DashEvent, AttackEvent;

    //combo actions
    public static event Action Healing, Poison, Neurotoxin, Teleport;

    public static bool slowPlayer;

    Animator anim;
    
#endregion

#region Init

    void Awake() {
        inputActions = new InputSystemActions();
    }
    void Start() {
        anim = transform.parent.gameObject.GetComponent<Animator>();
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
        look.started += Look;

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

#endregion

#region Input Voids

    void Dash(InputAction.CallbackContext context) {
        //Debug.Log("dash");
        DashEvent?.Invoke();
    }
    void Magic(InputAction.CallbackContext context) {
        //Debug.Log("magic call");
        MagicCall();
    }
    void Look(InputAction.CallbackContext context) {
        StartCoroutine(LookingLoop(LookCheck()));
    }
    void Attack(InputAction.CallbackContext context) {
        //Debug.Log("slash");
        AttackEvent?.Invoke();
    }

#endregion

#region Callback Voids

    void MagicCall() {      
        if (LookCheck() < 0 && CastCheck() > 0) {
            Healing?.Invoke();
            slowPlayer = true;
        }
        else {
            Poison?.Invoke();
        }
    }

    float LookCheck() {
        float looking = look.ReadValue<float>();
        return looking;
    }

    public float CastCheck() {
        float casting = magic.ReadValue<float>();
        return casting;
    }

    IEnumerator LookingLoop(float looking) {
        yield return new WaitForSeconds(Time.deltaTime);
        if (looking > 0) {
            //Debug.Log("look up");
            anim.SetBool("isLooking", true);
        }
        if (looking < 0) {
            //Debug.Log("crouch");
            anim.SetBool("isCrouching", true);
        }
        if (looking == 0) {
            slowPlayer = false;
        }

        if (looking != 0) {
            slowPlayer = true;
            StartCoroutine(LookingLoop(LookCheck()));
        }
        else {
            anim.SetBool("isLooking", false);
            anim.SetBool("isCrouching", false);
        }
    }

#endregion

}
