using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_spawner : MonoBehaviour
{
    public int openingDir;
    private int rand;
    private bool spawned = false;
    private string roomSpawned;

    private room_templates templates;

    void Start() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        Invoke("Spawn",0.1f);
    }

    void Spawn() {
        if(spawned == false) {
            if(openingDir == 1) {
                rand = Random.Range(0,templates.bottom_rooms.Length);
                Instantiate(templates.bottom_rooms[rand],transform.position, Quaternion.identity);
                roomSpawned = templates.bottom_rooms[rand].name;
            } else if(openingDir == 2) {
                rand = Random.Range(0,templates.top_rooms.Length);
                Instantiate(templates.top_rooms[rand],transform.position, Quaternion.identity);
                roomSpawned = templates.top_rooms[rand].name;
            } else if(openingDir == 3) {
                rand = Random.Range(0,templates.left_rooms.Length);
                Instantiate(templates.left_rooms[rand],transform.position, Quaternion.identity);
                roomSpawned = templates.left_rooms[rand].name;
            } else if(openingDir == 4) {
                rand = Random.Range(0,templates.right_rooms.Length);
                Instantiate(templates.right_rooms[rand],transform.position, Quaternion.identity);
                roomSpawned = templates.right_rooms[rand].name;
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("spawn_point") && spawned == false) {
            if(other.GetComponent<room_spawner>().spawned == false) {
                Invoke("BlockDoor",0.1f);
                other.GetComponent<room_spawner>().Invoke("BlockDoor",0.1f);
            }
            if(other.GetComponent<room_spawner>().spawned == true) {
                //if(openingDir == 1 && otherGetComponent<room_spawner>().roomSpawned == "") Invoke("BlockDoor",0.1f);
            }
            spawned = true;
        }
    }

    void BlockDoors() {
        Instantiate(templates.barriers[0], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void BlockDoor() {
        if(openingDir == 1) {
            Instantiate(templates.barriers[3],transform.position, Quaternion.identity);
        } else if(openingDir == 2) {
            Instantiate(templates.barriers[4],transform.position, Quaternion.identity);
        } else if(openingDir == 3) {
            Instantiate(templates.barriers[1],transform.position, Quaternion.identity);
        } else if(openingDir == 4) {
            Instantiate(templates.barriers[2],transform.position, Quaternion.identity);
        }
    }
}
