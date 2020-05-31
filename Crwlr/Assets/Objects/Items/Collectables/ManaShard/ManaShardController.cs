using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaShardController : MonoBehaviour
{
    public int shardValue;
    public float minAge = 1;
    private float Age;

    void Update() {
        Age += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player") && Age >= minAge) {
            ManaController playerMana = col.gameObject.GetComponent<ManaController>();
            if(playerMana.manaPoints < playerMana.maxManaPoints) {
                playerMana.manaPoints += shardValue;
                Destroy(gameObject);
            }
        }
    }
}
