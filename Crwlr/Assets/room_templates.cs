using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_templates : MonoBehaviour
{
    public GameObject[] bottom_rooms;
    public GameObject[] top_rooms;
    public GameObject[] left_rooms;
    public GameObject[] right_rooms;
    /*public GameObject[] not_bottom_rooms;
    public GameObject[] not_top_rooms;
    public GameObject[] not_left_rooms;
    public GameObject[] not_right_rooms;*/
    public GameObject[] barriers;

    public List<GameObject> rooms;

    public float waitTime;
    public bool spawnedBoss;
    public GameObject boss;

    void Update() {
        if(waitTime <= 0) {
            if(spawnedBoss == false) {
                Instantiate(boss, rooms[rooms.Count -1].transform.position, Quaternion.identity);
                boss.transform.position += new Vector3(0,0,10000);
                spawnedBoss = true;
            }
        }
        else waitTime -= Time.deltaTime;
    }
}
