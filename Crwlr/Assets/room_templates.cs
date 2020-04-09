using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_templates : MonoBehaviour
{
    public GameObject leftRoom, rightRoom, bottomRoom, topRoom;
    public GameObject leftBarrier, rightBarrier, bottomBarrier, topBarrier;
    public GameObject lockedRoom;
    public GameObject[] enemyPrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject[] itemPrefabs;

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
