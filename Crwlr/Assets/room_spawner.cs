using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_spawner : MonoBehaviour
{
    public string openingDir;
    private int rand;
    private bool spawned = false, collision = false;
    public float prob;
    public int maxRooms;
    float delay;

    private room_templates templates;

    void Start() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        delay = Random.Range(0.0f,0.5f);
        Invoke("SpawnDecision",delay);
    }

    void SpawnDecision() {
        prob = 1-templates.rooms.Count/maxRooms;
        if(Random.Range(0.0f,1.0f) < prob && !collision) SpawnRoom();
        else BlockDoor();
    }

    void SpawnRoom() {
        if(openingDir == "left") Instantiate(templates.rightRoom, transform.position, Quaternion.identity);
        if(openingDir == "right") Instantiate(templates.leftRoom, transform.position, Quaternion.identity);
        if(openingDir == "bottom") Instantiate(templates.topRoom, transform.position, Quaternion.identity);
        if(openingDir == "top") Instantiate(templates.bottomRoom, transform.position, Quaternion.identity);
        spawned = true;
    }

    void BlockDoor() {
        if(openingDir == "left") Instantiate(templates.rightBarrier, transform.position, Quaternion.identity);
        if(openingDir == "right") Instantiate(templates.leftBarrier, transform.position, Quaternion.identity);
        if(openingDir == "bottom") Instantiate(templates.topBarrier, transform.position, Quaternion.identity);
        if(openingDir == "top") Instantiate(templates.bottomBarrier, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("spawn_point") && !spawned) collision = true;
    }
}
