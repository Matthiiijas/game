using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_builder : MonoBehaviour
{
    private Transform player;
    private Vector2 playerPos;

    private room_templates templates;

    void Awake() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        templates.rooms.Add(this.gameObject);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        playerPos = player.position - transform.position;
        if(playerPos.x > -8 && playerPos.x < 8 && playerPos.y > -4 && playerPos.y < 4) LockRoom();
    }

    void LockRoom() {
        //Instantiate(templates.lockedRoom], transform.position, Quaternion.identity);
    }

}
