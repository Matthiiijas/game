using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_spawner : MonoBehaviour
{
    public string openingDir;
    private string otherDir;
    private int rand;
    private bool spawned = false;
    public float prob;

    private room_templates templates;

    void Start() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();

        if(Random.Range(0.0f,1.0f) < prob) Invoke("SpawnRoom",0.1f);
        else Invoke("BlockDoor",0.1f);
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
        if(other.CompareTag("spawn_point") && /* other.GetComponent<room_spawner>().spawned == true && */spawned == false) BlockDoor();
        /*if(other.CompareTag("spawn_point") && other.GetComponent<room_spawner>().spawned == false && spawned == false) {
            Destroy(other.gameObject);
            SpawnRoom();
            otherDir = other.GetComponent<room_spawner>().openingDir;
            Invoke("OpenUp",0.1f);
        }*/
    }

    /*void OpenUp() {
        if(otherDir == "left") Destroy(GameObject.Find)
    }*/
}
