using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBarManager : MonoBehaviour
{
    public SpriteRenderer[] shards;
    public Sprite fullShard, halfShard, emptyShard;
    ManaController playerMana;

    public int manaPoints;
    public int numShards;
    public int maxShards;

    void Start() {
        shards = GetComponentsInChildren<SpriteRenderer>();
        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaController>();
    }


    void Update() {
        manaPoints = playerMana.manaPoints;
        numShards = playerMana.manaPoints / 2;
        maxShards = playerMana.maxManaPoints / 2;

        for (int i = 0; i < shards.Length; i++) {
            if(i < numShards) {
                shards[i].sprite = fullShard;
            }
            else if(i == numShards && manaPoints%2 == 1) shards[i].sprite = halfShard;
            else shards[i].sprite = emptyShard;

            if(i < maxShards) shards[i].enabled = true;
            else shards[i].enabled = false;
        }
    }
}
