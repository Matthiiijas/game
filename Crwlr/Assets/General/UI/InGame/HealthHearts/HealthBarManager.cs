using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public SpriteRenderer[] hearts;
    public Sprite fullHeart, halfHeart, emptyHeart;
    HealthController playerHealth;

    public int healthPoints;
    public int numHearts;
    public int maxHearts;

    void Start() {
        hearts = GetComponentsInChildren<SpriteRenderer>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
    }


    void Update() {
        healthPoints = playerHealth.healthPoints;
        numHearts = playerHealth.healthPoints / 2;
        maxHearts = playerHealth.maxHealthPoints / 2;

        for (int i = 0; i < hearts.Length; i++) {
            if(i < numHearts) {
                hearts[i].sprite = fullHeart;
            }
            else if(i == numHearts && healthPoints%2 == 1) hearts[i].sprite = halfHeart;
            else hearts[i].sprite = emptyHeart;

            if(i < maxHearts) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }
    }
}
