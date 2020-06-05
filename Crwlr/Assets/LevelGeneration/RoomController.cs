using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    West, North, East, South, All
}

public class RoomController : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorBot, doorTop;

    Transform player;
    Vector2 playerPos;
    Direction playerLeft;
    GameManager gameManager;

    [Header("Player interaction")]
    [Tooltip("How far player is teleported when leaving a room")]
    public float transitionDistance = 1.6f;

    [Space(10)]
    public Room refRoom;
    [Header("Current room states")]
    [Tooltip("Type of room")]
    public roomType type;
    [Tooltip("Player is inside this room")]
    public bool playerInRoom = false;
    [Tooltip("Enemies in this room are defeated")]
    public int enemyCount;
    public bool cleared = false;

    public ObjectTable[] enemyPresets;
    public GameObject chestObject;
    public GameObject bossObject;

    public GameObject roomContent;

    void Start() {
        //Get GameObject Components
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //Setup refence Room class and roomtype
        if(refRoom == null) refRoom = new Room(transform.position, type);
        else type = refRoom.type;
        //Disable doors
        if(!refRoom.doorLeft) DisableDoor(doorLeft);
        if(!refRoom.doorRight) DisableDoor(doorRight);
        if(!refRoom.doorBot) DisableDoor(doorBot);
        if(!refRoom.doorTop) DisableDoor(doorTop);
        //Set Boss door
        if(refRoom.doorBossLeft) BossDoor(Direction.West);
        if(refRoom.doorBossRight) BossDoor(Direction.East);
        if(refRoom.doorBossBot) BossDoor(Direction.South);
        if(refRoom.doorBossTop) BossDoor(Direction.North);
        //Spawn room contents
        if(type == roomType.Enemy) roomContent = Instantiate(ObjectTable.GetRandom(enemyPresets), transform);
        if(type == roomType.Chest) roomContent = Instantiate(chestObject, transform);
        if(type == roomType.Boss) {
            roomContent = Instantiate(bossObject, transform);
            roomContent.SetActive(false);
        }
    }

    void Update() {
        //Define playsers position relative to room
        playerPos = (Vector2) (player.position - transform.position);

        enemyCount = 0;
        foreach(Transform child in transform) {
            if(child.CompareTag("Enemy")) enemyCount++;
        }

        if(playerInRoom) {
            if(enemyCount >= 1) {
                Door(Direction.All, "Close");
            }
            else {
                Door(Direction.All, "Open");
                cleared = true;
            }
        }

        if(type == roomType.Boss && playerInRoom) {
            if(roomContent == null) gameManager.GoToMainMenu();
            roomContent.SetActive(true);
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

    void BossDoor(Direction direction) {
        switch(direction) {
            case Direction.West:
                doorLeft.transform.Find("BossFrame").gameObject.SetActive(true);
                break;
            case Direction.East:
                doorRight.transform.Find("BossFrame").gameObject.SetActive(true);
                break;
            case Direction.South:
                doorBot.transform.Find("BossFrame").gameObject.SetActive(true);
                break;
            case Direction.North:
                doorTop.transform.Find("BossFrame").gameObject.SetActive(true);
                break;
        }
    }

    void DisableDoor(GameObject door) {
        door.GetComponent<Animator>().SetBool("Inactive", true);
        door.GetComponent<SpriteRenderer>().enabled = false;
        door.transform.Find("DoorMask").gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))  playerInRoom = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) playerInRoom = false;
    }
}
