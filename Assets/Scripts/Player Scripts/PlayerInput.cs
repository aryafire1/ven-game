using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using System;

public class PlayerInput : MonoBehaviour
{
#region Variables

    //mobility
    public float speed, sprintSpeed;
    public float force, dashForce;
    float direction, sprinting;
    int jumping;
    public static bool slowPlayer;

    //input system
    public InputSystemActions inputActions;
    InputAction move, jump, sprint, dash, magic, look, attack;

    //needed components
    Rigidbody rb;
    SpriteRenderer renderer;
    Animator anim;

    //input actions
    public static event Action DashEvent, AttackEvent, InteractEvent;
    [HideInInspector]
    public bool interacting;

    //combo actions
    public static event Action Healing, Poison, Neurotoxin, Teleport;

    //camera control
    Vector3 playerWidth;
    [HideInInspector]
    public bool inBounds;

#endregion

#region Init

    void Awake() {
        inputActions = new InputSystemActions();
        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void OnEnable() {
        move = inputActions.Player.Move;
        move.Enable();

        sprint = inputActions.Player.Sprint;
        sprint.Enable();
        sprint.performed += Sprint;

        jump = inputActions.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

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
        move.Disable();
        jump.Disable();
        dash.Disable();
        magic.Disable();
        look.Disable();
        attack.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerWidth = renderer.size;
    }

#endregion

#region Input Voids

    void Update() {
        direction = move.ReadValue<float>();
        sprinting = sprint.ReadValue<float>();

        if (direction != 0 && slowPlayer == false) {
            Move();
        }
        else {
            anim.SetBool("isRunning", false);
        }
    }

    void Move() {
            if (direction > 0) {
                renderer.flipX = true;
            }
            if (direction < 0) {
                renderer.flipX = false;
            }
            anim.SetBool("isRunning", true);
            transform.Translate(direction * speed * Vector3.right * Time.deltaTime);
    }

    void Sprint(InputAction.CallbackContext context) {
        StartCoroutine(SprintingLoop());
    }

    void Jump(InputAction.CallbackContext context) {
        if (jumping < 2) {
            anim.SetTrigger("isJumping");
            rb.velocity = rb.velocity + (force * Vector3.up);
            anim.SetBool("isFalling", true);
            jumping++;
        }
    }

    void Dash(InputAction.CallbackContext context) {
        //Debug.Log("dash");
        DashEvent?.Invoke();
        if (direction == 0) {
            direction = 1;
        }
        rb.velocity = rb.velocity + (dashForce * direction * Vector3.right);
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
        if (interacting) {
            InteractEvent?.Invoke();
        }
        else {
            AttackEvent?.Invoke();
        }
    }

#endregion

#region Callback Voids

    /* void Dash() {
        //Debug.Log("dash");
        if (direction == 0) {
            direction = 1;
        }
        rb.velocity = rb.velocity + (dashForce * direction * Vector3.right);
    } */

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

#endregion

#region Misc

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Floor")) {
            anim.SetBool("isFalling", false);
            jumping = 0;

            if (anim.GetBool("isRunning") == true) {
                StartCoroutine(SprintingLoop());
            }
        }
    }


    IEnumerator SprintingLoop() {
        yield return new WaitForSeconds(Time.deltaTime);

        if (sprinting > 0 && jumping == 0 && EventManager.slowPlayer == false) { //<- good god
            rb.velocity = direction * sprintSpeed * Vector3.right;
            anim.SetBool("isSprinting", true);

            StartCoroutine(SprintingLoop());
        }
        else {
            anim.SetBool("isSprinting", false);
        }
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
