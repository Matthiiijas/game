using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public SpriteRenderer[] hearts;
    public Sprite fullHeart, halfHeart, emptyHeart;
    DamageManager playerDamage;

    public int healthPoints;
    public int numHearts;
    public int maxHearts;

    void Start() {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageManager>();
    }


    void Update() {
        healthPoints = playerDamage.healthPoints;
        numHearts = playerDamage.healthPoints / 2;
        maxHearts = playerDamage.maxHealthPoints / 2;

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
