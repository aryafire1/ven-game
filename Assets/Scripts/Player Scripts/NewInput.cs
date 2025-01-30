using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class NewInput : MonoBehaviour
{

    public float speed;
    public float force;
    public float dashForce;

    public InputSystemActions inputActions;
    InputAction move;
    InputAction jump;

    float direction;
    Rigidbody rb;
    Animator anim;
    SpriteRenderer renderer;


    void Awake() {
        inputActions = new InputSystemActions();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        move = inputActions.Player.Move;
        move.Enable();

        jump = inputActions.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    void OnDisable() {
        move.Disable();
        jump.Disable();

        EventManager.DashEvent -= Dash;
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        EventManager.DashEvent += Dash;
    }

    void Update() {
        direction = move.ReadValue<float>();
        if (direction != 0 && EventManager.slowPlayer == false) {
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

    void Jump(InputAction.CallbackContext context) {
        anim.SetTrigger("isJumping");
        rb.velocity = force * Vector3.up;
        anim.SetBool("isFalling", true);
    }

    void Dash() {
        Debug.Log("dash");
        if (direction == 0) {
            direction = 1;
        }
        rb.velocity = dashForce * direction * Vector3.right;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Floor")) {
            anim.SetBool("isFalling", false);
        }
    }
}
