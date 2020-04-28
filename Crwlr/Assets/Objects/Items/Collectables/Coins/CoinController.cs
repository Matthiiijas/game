using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int coinValue;
    public float minAge = 0.5f;
    private float Age = 0;

    void Update() {
        Age += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player") && Age >= minAge) {
            col.gameObject.GetComponent<PlayerController>().moneyCount += coinValue;
            Destroy(gameObject);
        }
    }
}
