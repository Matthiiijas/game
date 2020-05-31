using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContentGenerator : MonoBehaviour
{
    public bool Active;
    public Vector2[] spawnPositions;
    public ObjectTable[] objectsToSpawn;

    void OnDrawGizmosSelected() {
        for(int i = 0; i < spawnPositions.Length; i++) {
            Gizmos.DrawIcon(spawnPositions[i] + (Vector2) transform.position, "sv_icon_dot3_pix16_gizmo", true);
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(16,9,0));
    }

    public void SpawnEnemies() {
        for(int i = 0; i < spawnPositions.Length; i++) {
            GameObject curObject = Instantiate(ObjectTable.GetRandom(objectsToSpawn), spawnPositions[i] + (Vector2) transform.position, Quaternion.identity);
            curObject.transform.SetParent(gameObject.transform);
        }
        Active = true;
    }
}
