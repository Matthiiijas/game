using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_builder : MonoBehaviour
{
    private Transform player;
    private Vector2 playerPos;

    private bool locked = false;
    private bool cleared = false;
    private int rand;

    private room_templates templates;

    void Awake() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Add this room to list of rooms
        templates.rooms.Add(this.gameObject);
    }

    void Update() {
        //Get players position relative to this room
        playerPos = player.position - transform.position;
        //Debug.Log(gameObject.name + " is locked: " + locked);
        cleared = true;
        if(locked) {
            if(cleared) Debug.Log("cleared");
        }

    }

    void LockRoom() {
        if(!locked) {

            if(Random.Range(0.0f,1.0f) < 0.8f) {
                Instantiate(templates.lockedRoom, transform.position, Quaternion.identity);
                rand = Random.Range(0,templates.enemyPrefabs.Length);
                Instantiate(templates.enemyPrefabs[rand], transform.position, Quaternion.identity);
            }
            //Instantiate(templates.obstaclePrefabs[Random.Range(0,templates.obstaclePrefabs.Length)], transform.position, Quaternion.identity);
        }
        locked = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) LockRoom();
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("enemy")) cleared = false;
        Debug.Log("enemy");
    }

}
