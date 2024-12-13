using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    InputAction move, attack, dash, look, jump;

    // Start is called before the first frame update
    void Start()
    {
        if (InputSystem.actions) {
            move = InputSystem.actions.FindAction("Player/Move");
            attack = InputSystem.actions.FindAction("Player/Attack");
            dash = InputSystem.actions.FindAction("Player/Dash");
            look = InputSystem.actions.FindAction("Player/Look");
            jump = InputSystem.actions.FindAction("Player/Jump");
        }
    }
}
