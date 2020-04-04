using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_spawner : MonoBehaviour
{
    public int openingDir;
    private int rand;
    private bool spawned = false;

    private room_templates templates;

    void Start() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        Invoke("Spawn",0.3f);
    }

    void Spawn() {
        if(spawned == false) {
            if(openingDir == 1) {
                rand = Random.Range(0,templates.bottom_rooms.Length);
                Instantiate(templates.bottom_rooms[rand],transform.position, Quaternion.identity);
            } else if(openingDir == 2) {
                rand = Random.Range(0,templates.top_rooms.Length);
                Instantiate(templates.top_rooms[rand],transform.position, Quaternion.identity);
            } else if(openingDir == 3) {
                rand = Random.Range(0,templates.left_rooms.Length);
                Instantiate(templates.left_rooms[rand],transform.position, Quaternion.identity);
            } else if(openingDir == 4) {
                rand = Random.Range(0,templates.right_rooms.Length);
                Instantiate(templates.right_rooms[rand],transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("spawn_point")) {
            Destroy(gameObject);
        }
    }
}
