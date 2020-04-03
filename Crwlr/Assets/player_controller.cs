using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    Animator anim;
    InputMaster controls;
    Vector2 movement;

    public float speed;

    Rigidbody2D rb;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        controls = new InputMaster();
        controls.player.move.performed += ctx => movement = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate () {
        rb.velocity = movement*speed;
        if (rb.velocity.magnitude > 0) anim.Play("walk_front");
        else anim.Play("idle_front");
    }

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
