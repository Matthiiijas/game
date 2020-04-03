using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    InputMaster controls;
    Vector2 movement;

    public float speed;
    public string orientation;


    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        controls = new InputMaster();
        controls.player.move.performed += ctx => movement = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate () {
        //Movement
        rb.velocity = movement*speed;

        //Get direction
        float angle = Vector2.SignedAngle(movement,new Vector2(1,0));
        Debug.Log(angle);
        if(movement != Vector2.zero) {
            if(angle < -135) orientation = "left";
            else if(angle <= -45) orientation = "up";
            else if(angle < 45) orientation = "right";
            else if(angle <= 135) orientation = "down";
            else orientation = "left";
        }

        //Animation
        if(orientation == "left") {
            sr.flipX = true;
            if(rb.velocity.magnitude > 0) anim.Play("idle_side"); // walk_side
            else anim.Play("idle_side");
            }
        if(orientation == "right") {
            sr.flipX = false;
            if(rb.velocity.magnitude > 0) anim.Play("idle_side"); //walk_side
            else anim.Play("idle_side");
            }
        if(orientation == "up") {
            sr.flipX = false;
            if(rb.velocity.magnitude > 0) anim.Play("walk_back");
            else anim.Play("idle_back");
            }
        if(orientation == "down") {
            sr.flipX = false;
            if(rb.velocity.magnitude > 0) anim.Play("walk_front");
            else anim.Play("idle_front");
            }
    }

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
