using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction {
    West, North, East, South,
}

public class room_setup : MonoBehaviour
{
    GameObject doorLeft, doorRight, doorBot, doorTop;

    Transform player;
    Vector2 playerPos;
    Direction playerLeft;

    public bool roomActive;

    Vector2 boxCastOffset;
    public Vector2 roomSize;
    public LayerMask Rooms;

    public Room refRoom;
    public GameObject enemy;

    void Start() {
        //Get door objects
        doorLeft = transform.Find("DoorLeft").gameObject;
        doorRight = transform.Find("DoorRight").gameObject;
        doorBot = transform.Find("DoorBot").gameObject;
        doorTop = transform.Find("DoorTop").gameObject;
        //Propietery: Disable doors
        if(!refRoom.doorTop) {
            doorTop.GetComponent<SpriteRenderer>().enabled = false;
            doorTop.GetComponent<Animator>().SetTrigger("Close");
        }
        if(!refRoom.doorBot) {
            doorBot.GetComponent<SpriteRenderer>().enabled = false;
            doorBot.GetComponent<Animator>().SetTrigger("Close");
        }
        if(!refRoom.doorLeft) {
            doorLeft.GetComponent<SpriteRenderer>().enabled = false;
            doorLeft.GetComponent<Animator>().SetTrigger("Close");
        }
        if(!refRoom.doorRight) {
            doorRight.GetComponent<SpriteRenderer>().enabled = false;
            doorRight.GetComponent<Animator>().SetTrigger("Close");
        }

        if(refRoom.type == roomType.Enemy) Instantiate(enemy,transform);
        //Get player transform for transition
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    void Update() {
        if(roomActive) {
            if(Input.GetKey("f")) Door(Direction.West,"Close",true);
            if(Input.GetKey("h")) Door(Direction.East,"Close",true);
            if(Input.GetKey("g")) Door(Direction.South,"Close",true);
            if(Input.GetKey("t")) Door(Direction.North,"Close",true);
        }
        //Define playsers position relative to room
        playerPos = (Vector2) (player.position - transform.position);

    }

    //Close specific door (or all)
    void Door(Direction direction, string action, bool first) {
        switch(direction) {
            case Direction.West:
                doorLeft.GetComponent<Animator>().SetTrigger(action);
                //if(first) roomLeft.GetComponent<room_setup>().Door(Direction.East,action,false);
                break;
            case Direction.East:
                doorRight.GetComponent<Animator>().SetTrigger(action);
                //if(first) roomRight.GetComponent<room_setup>().Door(Direction.West,action,false);
                break;
            case Direction.South:
                doorBot.GetComponent<Animator>().SetTrigger(action);
                //if(first) roomBot.GetComponent<room_setup>().Door(Direction.North,action,false);
                break;
            case Direction.North:
                doorTop.GetComponent<Animator>().SetTrigger(action);
                //if(first) roomTop.GetComponent<room_setup>().Door(Direction.South,action,false);
                break;
        }
    }
    //Transition from one to another room
    void Transition(Direction direction) {
        switch(direction) {
            case Direction.West:
                player.position += new Vector3(-1.5f,0,0); break;
            case Direction.East:
                player.position += new Vector3(1.5f,0,0); break;
            case Direction.South:
                player.position += new Vector3(0,-1.5f,0); break;
            case Direction.North:
                player.position += new Vector3(0,1.5f,0); break;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            roomActive = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            roomActive = false;
            if(playerPos.x < -1) playerLeft = Direction.West;
            else if(playerPos.x > 1) playerLeft = Direction.East;
            else if(playerPos.y < -1) playerLeft = Direction.South;
            else if(playerPos.y > 1) playerLeft = Direction.North;
            Transition(playerLeft);
        }
    }
}
