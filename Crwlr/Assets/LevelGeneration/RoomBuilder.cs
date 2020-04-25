using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction {
    West, North, East, South, All
}

public class RoomBuilder : MonoBehaviour
{
    GameObject doorLeft, doorRight, doorBot, doorTop;

    Transform player;
    Vector2 playerPos;
    Direction playerLeft;

    [Header("Player interaction")]
    [Tooltip("How far player is teleported when leaving a room")]
    public float transitionDistance = 1.6f;

    [Space(10)]
    [HideInInspector] public Room refRoom;
    [Header("Current room states")]
    [Tooltip("Type of room")]
    public roomType type;
    [Tooltip("Player is inside this room")]
    public bool roomActive = false;
    [Tooltip("Enemies in this room ar defeated")]
    public bool cleared = false;

    [Space(10)]
    [Header("Enemy Prefabs to use if type is \"Enemy\"")]
    public GameObject enemy;
    public GameObject boss;
    [Space(10)]
    [Tooltip("Instantiated Enemy Prefab")]
    public GameObject enemySet;

    /*Vector2 boxCastOffset;
    public Vector2 roomSize;
    public LayerMask Rooms;*/

    void Start() {
        if(refRoom == null) refRoom = new Room(transform.position, type);
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
        else if(type == roomType.Boss) {
            enemySet = Instantiate(boss,transform);
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

        if(enemySet != null) {
            if(!enemySet.GetComponent<EnemySetManager>().Active && roomActive) enemySet.GetComponent<EnemySetManager>().Active = true;
            if(enemySet.transform.childCount == 0) {
                Door(Direction.All, "Open");
                cleared = true;
            }
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
                player.position += new Vector3(-transitionDistance,0,0); break;
            case Direction.East:
                player.position += new Vector3(transitionDistance,0,0); break;
            case Direction.South:
                player.position += new Vector3(0,-transitionDistance,0); break;
            case Direction.North:
                player.position += new Vector3(0,transitionDistance,0); break;
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
