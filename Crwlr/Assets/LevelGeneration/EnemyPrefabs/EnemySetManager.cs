using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetManager : MonoBehaviour
{
    public bool Active;
    public Vector2[] spawnPositions;
    public GameObject[] enemiesToSpawn;

    void OnDrawGizmosSelected() {
        for(int i = 0; i < spawnPositions.Length; i++) {
            Gizmos.DrawIcon(spawnPositions[i] + (Vector2) transform.position, "sv_icon_dot3_pix16_gizmo", true);
        }
    }

    public void SpawnEnemies() {
        for(int i = 0; i < spawnPositions.Length; i++) {
            GameObject curObject = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], spawnPositions[i] + (Vector2) transform.position, Quaternion.identity);
            curObject.transform.SetParent(gameObject.transform);
        }
        Active = true;
    }
}
