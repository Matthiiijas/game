using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_setup : MonoBehaviour
{
    private Transform player;
    private Vector2 playerPos;

    private room_templates templates;

    void Start() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        playerPos = player.position - transform.position;
        if(playerPos.x > -8 && playerPos.y < 8 && playerPos.y > -4 && playerPos.y < 4) LockRoom();
    }

    void LockRoom() {
        //Instantiate(templates.barrier[5], transform.position, Quaternion.identity);
    }
}
