using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderManager : MonoBehaviour
{
    public float offset;
    Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update() {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt((transform.position.y - player.position.y + offset) * 10f) * -1;
    }
}
