using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_builder : MonoBehaviour
{
    private GameObject player;
    private Vector2 playerPos;
    public locker Locker;

    private int rand;
    public bool cleared = false;

    GameObject enemies;
    int enemyCount;

    private room_templates templates;

    void Awake() {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        player = GameObject.FindGameObjectWithTag("Player");
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
            player.GetComponent<player_controller>().transitioning = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) player.GetComponent<player_controller>().transitioning = true;
    }

}
