using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction {
    West, North, East, South, All
}

public class room_setup : MonoBehaviour
{
    GameObject doorLeft, doorRight, doorBot, doorTop;

    Transform player;
    Vector2 playerPos;
    Direction playerLeft;

    public bool roomActive = false, cleared = false;

    Vector2 boxCastOffset;
    public Vector2 roomSize;
    public LayerMask Rooms;

    public Room refRoom;
    public roomType type;
    public GameObject enemy, enemySet;

    void Start() {
        type = refRoom.type;
        //Get door objects
        doorLeft = transform.Find("DoorLeft").gameObject;
        doorRight = transform.Find("DoorRight").gameObject;
        doorBot = transform.Find("DoorBot").gameObject;
        doorTop = transform.Find("DoorTop").gameObject;
        //Disable doors
        if(!refRoom.doorLeft) DisableDoor(doorLeft);
        if(!refRoom.doorRight) DisableDoor(doorRight);
        if(!refRoom.doorBot) DisableDoor(doorBot);
        if(!refRoom.doorTop) DisableDoor(doorTop);

        if(type == roomType.Enemy) {
            enemySet = Instantiate(enemy,transform);
        }
        //Get player transform for transition
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    void Update() {
        if(roomActive) {
            if(Input.GetKey("f")) Door(Direction.West,"Close");
            if(Input.GetKey("h")) Door(Direction.East,"Close");
            if(Input.GetKey("g")) Door(Direction.South,"Close");
            if(Input.GetKey("t")) Door(Direction.North,"Close");
        }
        //Define playsers position relative to room
        playerPos = (Vector2) (player.position - transform.position);

        if(enemySet != null) if(enemySet.transform.childCount == 0) {
            Door(Direction.All, "Open");
            cleared = true;
        }
    }

    //Close specific door (or all)
    void Door(Direction direction, string action) {
        switch(direction) {
            case Direction.West:
                doorLeft.GetComponent<Animator>().SetTrigger(action);
                break;
            case Direction.East:
                doorRight.GetComponent<Animator>().SetTrigger(action);
                break;
            case Direction.South:
                doorBot.GetComponent<Animator>().SetTrigger(action);
                break;
            case Direction.North:
                doorTop.GetComponent<Animator>().SetTrigger(action);
                break;
            case Direction.All:
                doorLeft.GetComponent<Animator>().SetTrigger(action);
                doorRight.GetComponent<Animator>().SetTrigger(action);
                doorBot.GetComponent<Animator>().SetTrigger(action);
                doorTop.GetComponent<Animator>().SetTrigger(action);
                break;
        }
    }

    void DisableDoor(GameObject door) {
        door.GetComponent<Animator>().SetTrigger("Close");
        door.GetComponent<SpriteRenderer>().enabled = false;
        door.transform.Find("DoorMask").gameObject.SetActive(false);
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
            if(!cleared && type == roomType.Enemy) Door(Direction.All, "Close");
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
