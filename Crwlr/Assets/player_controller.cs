using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    InputMaster controls;
    Vector2 movement,moveremain = new Vector2(0,-1), vel;
    bool attack = false;

    public float speed;
    public string orientation;


    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        controls = new InputMaster();
        controls.player.move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.player.Attack.started += ctx => attack = true;
        controls.player.Attack.canceled += ctx => attack = false;
    }

    void FixedUpdate () {
        if(movement != Vector2.zero) moveremain = movement;
        anim.SetFloat("MoveX", rb.velocity.x);
        anim.SetFloat("MoveY", rb.velocity.y);
        anim.SetFloat("LookX", moveremain.x);
        anim.SetFloat("LookY", moveremain.y);
        anim.SetFloat("Velocity", movement.magnitude);
        anim.SetBool("Attack", attack);
        //Movement
        movement = Vector2.ClampMagnitude(movement,1);
        rb.velocity = movement*speed;
    }

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
