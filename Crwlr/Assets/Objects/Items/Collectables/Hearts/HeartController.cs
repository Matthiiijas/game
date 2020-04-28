using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public int heartValue;
    public float minAge = 1;
    private float Age;

    void Update() {
        Age += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player") && Age >= minAge) {
            DamageManager playerDamage = col.gameObject.GetComponent<DamageManager>();
            if(playerDamage.healthPoints < playerDamage.maxHealthPoints) {
                playerDamage.healthPoints += heartValue;
                Destroy(gameObject);
            }
        }
    }
}
