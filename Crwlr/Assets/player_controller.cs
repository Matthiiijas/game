using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    InputMaster controls;
    Vector2 movement;

    public float speed;

    Rigidbody2D rb;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();

        controls = new InputMaster();
        controls.player.move.performed += ctx => movement = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate () {
        Debug.Log(movement.magnitude);
        rb.velocity = movement*speed;
    }

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
