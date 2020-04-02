using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float speed;
    public Vector2 movement,movement_raw;
    public Vector2 orientation_raw;
    public string orientation;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Movement
        movement_raw = new Vector2(Input.GetAxis("MoveX")*speed,Input.GetAxis("MoveY")*speed);
        movement = Vector2.ClampMagnitude(movement_raw, 10);
        rb2d.velocity = movement;


        //Orientation
        orientation_raw = new Vector2(Input.GetAxis("LookX"),Input.GetAxis("LookY"));
        float angle = Vector2.SignedAngle(orientation_raw,new Vector2(1,0));
        if(angle < -135) orientation = "left";
        else if(angle < -45) orientation = "down";
        else if(angle < 45) orientation = "right";
        else if(angle < 135) orientation = "up";
        else orientation = "left";

        /*switch(orientation) {
            case "left": Animator.Play("idle");
            ...
        }*/
    }
}
