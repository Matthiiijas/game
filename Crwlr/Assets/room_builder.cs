using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_builder : MonoBehaviour
{
    private Vector2 playerPos;
    public locker Locker;

    private int rand;
    public bool cleared = false;

    GameObject enemies;
    int enemyCount;

    private room_templates templates;

    void Awake() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        Locker = transform.Find("locked_room").gameObject.GetComponent<locker>();

        //Add this room to list of rooms
        templates.rooms.Add(this.gameObject);

        rand = Random.Range(0,templates.enemyPrefabs.Length);
        enemies = Instantiate(templates.enemyPrefabs[rand], transform);
    }

    void LateUpdate() {
        if(enemies.GetComponent<enemy_counter>().enemyList.Count == 0) {
            Locker.Open();
            cleared = true;
            //Destroy(enemies.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            if(!cleared) Locker.Close();
            other.GetComponent<player_controller>().transitioning = false;
            //enemies.GetComponent<enemy_counter>().Enable();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) other.GetComponent<player_controller>().transitioning = true;
    }

}
