using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldInput : MonoBehaviour
{
    public float speed;
    public float force;

    Rigidbody rb;

    [HideInInspector]
    public bool crouching, lookup;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }

    void FixedUpdate() {
        Jump();
    }

    void Move() {
        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void Look() {
        if (Input.GetKey(KeyCode.W)) {
            lookup = true;
        }
        else if (Input.GetKeyUp(KeyCode.W)) {
            lookup = false;
        }
        if (Input.GetKey(KeyCode.S)) {
            crouching = true;
            //Debug.Log(crouching);
        }
        else if (Input.GetKeyUp(KeyCode.S)) {
            crouching = false;
            //Debug.Log(crouching);
        }
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Jump");
            rb.velocity = force * Vector3.up;
        }
    }
}
