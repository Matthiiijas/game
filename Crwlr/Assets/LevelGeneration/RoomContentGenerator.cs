using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContentGenerator : MonoBehaviour
{
    public List<Vector2> spawnPositions;
    public int NumOfRandomPositions;
    public Vector2 randomPositionBoundries = new Vector2(13.5f, 6.5f);

    public ObjectTable[] objectsToSpawn;

    void OnDrawGizmosSelected() {
        for(int i = 0; i < spawnPositions.Count; i++) {
            Gizmos.DrawIcon(spawnPositions[i] + (Vector2) transform.position, "sv_icon_dot3_pix16_gizmo", true);
        }
        Gizmos.DrawWireCube(transform.position, (Vector3)randomPositionBoundries);
    }

    void Start() {
        for(int i = 0; i < NumOfRandomPositions; i++) {
            spawnPositions.Add(new Vector2(randomPositionBoundries.x * (Random.value-0.5f), randomPositionBoundries.y * (Random.value-0.5f)));
        }
    }

    public void Activate() {
        for(int i = 0; i < spawnPositions.Count; i++) {
            GameObject enemyToSpawn = ObjectTable.GetRandom(objectsToSpawn);
            if(enemyToSpawn != null) {
                GameObject curObject = Instantiate(enemyToSpawn, spawnPositions[i] + (Vector2) transform.position, Quaternion.identity);
                curObject.transform.SetParent(gameObject.transform.parent);
            }
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) Activate();
    }
}
