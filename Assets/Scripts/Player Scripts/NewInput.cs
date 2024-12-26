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
    InputAction attack;
    InputAction jump;
    InputAction look;

    float direction;
    Rigidbody rb;



    void Awake() {
        inputActions = new InputSystemActions();
    }

    void OnEnable() {
        move = inputActions.Player.Move;
        move.Enable();

        attack = inputActions.Player.Attack;
        attack.Enable();
        attack.performed += Attack;

        jump = inputActions.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

        look = inputActions.Player.Look;
        look.Enable();
        look.performed += Look;
    }

    void OnDisable() {
        move.Disable();
        attack.Disable();
        jump.Disable();
        look.Disable();

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
    }

    void FixedUpdate() {
        Move();
    }



    void Attack(InputAction.CallbackContext context) {
        Debug.Log("slash");
    }

    void Move() {
        transform.Translate(direction * speed * Vector3.right * Time.deltaTime);
    }

    void Jump(InputAction.CallbackContext context) {
        rb.velocity = force * Vector3.up;
    }

    void Look(InputAction.CallbackContext context) {
        float looking = look.ReadValue<float>();
        if (looking > 0) {
            Debug.Log("look up");
        }
        if (looking < 0) {
            Debug.Log("crouch");
        }
    }

    void Dash() {
        Debug.Log("dash");
        if (direction == 0) {
            direction = 1;
        }
        rb.velocity = dashForce * direction * Vector3.right;
    }
}
