using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public SpriteRenderer[] hearts;
    public Sprite fullHeart, halfHeart, emptyHeart;

    public int healthPoints;
    public int maxHearts;


    void Update() {
        healthPoints = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageManager>().healthPoints;

        for (int i = 0; i < hearts.Length; i++) {
            if(2*i < healthPoints) {
                hearts[i].sprite = fullHeart;
                if(healthPoints%2 == 1) hearts[healthPoints/2].sprite = halfHeart;
            }
            else hearts[i].sprite = emptyHeart;

            if(i < maxHearts) hearts[i].enabled = true;
            else hearts[i].enabled = false;
        }
    }
}
