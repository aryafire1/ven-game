using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldInput : MonoBehaviour
{
    public float speed;
    public float force;

    Rigidbody rb;

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
        if (Input.GetKeyDown(KeyCode.W)) {
            Debug.Log("Look up");
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("Crouch");
        }
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Jump");
            rb.velocity = force * Vector3.up;
        }
    }
}
