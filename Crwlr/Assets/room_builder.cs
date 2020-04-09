using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_builder : MonoBehaviour
{
    private Transform player;
    private Vector2 playerPos;

    private bool locked = false;

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
        //Determine if player is inside this room
        if(playerPos.x > -8 && playerPos.x < 8 && playerPos.y > -4 && playerPos.y < 4) Invoke("LockRoom",0.1f);
        //Debug.Log(gameObject.name + " is locked: " + locked);
    }

    void LockRoom() {
        if(locked == false) {

            if(Random.Range(0.0f,1.0f) < 0.8f) {
                Instantiate(templates.lockedRoom, transform.position, Quaternion.identity);
                Instantiate(templates.enemyPrefabs[Random.Range(0,templates.enemyPrefabs.Length)], transform.position, Quaternion.identity);
            }
            //Instantiate(templates.obstaclePrefabs[Random.Range(0,templates.obstaclePrefabs.Length)], transform.position, Quaternion.identity);
        }

        locked = true;
    }

}
