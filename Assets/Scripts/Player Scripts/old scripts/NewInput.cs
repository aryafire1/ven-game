using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class NewInput : MonoBehaviour
{

#region Variables

    public float speed, sprintSpeed;
    public float force, dashForce;

    public InputSystemActions inputActions;
    InputAction move, jump, sprint;

    float direction, sprinting;
    int jumping;
    Rigidbody rb;
    SpriteRenderer renderer;
    Animator anim;

    Vector3 playerWidth;
    [HideInInspector]
    public bool inBounds;

#endregion

#region Init

    void Awake() {
        inputActions = new InputSystemActions();
        renderer = GetComponent<SpriteRenderer>();
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
    }

    void OnDisable() {
        move.Disable();
        jump.Disable();

        EventManager.DashEvent -= Dash;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        EventManager.DashEvent += Dash;
        playerWidth = renderer.size;
    }

    void Update() {
        direction = move.ReadValue<float>();
        sprinting = sprint.ReadValue<float>();

        if (direction != 0 && EventManager.slowPlayer == false) {
            Move();
        }
        else {
            anim.SetBool("isRunning", false);
        }
    }

#endregion

#region Input Voids

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

    void Dash() {
        //Debug.Log("dash");
        if (direction == 0) {
            direction = 1;
        }
        rb.velocity = rb.velocity + (dashForce * direction * Vector3.right);
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

#endregion
}
