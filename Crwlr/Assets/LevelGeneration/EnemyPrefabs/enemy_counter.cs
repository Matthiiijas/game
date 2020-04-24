using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_counter : MonoBehaviour
{
    public List<GameObject> enemyList;

    void Update() {
        enemyList.Clear();
        foreach (Transform child in transform) {
            enemyList.Add(child.gameObject);
        }
    }
}
