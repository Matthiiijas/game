using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    Transform player;
    Vector3 roundPlayerPos;

    [Tooltip("How fast camera shall roll to the next room")]
    [Range(0,1)]
    public float camSmooth = 0.5f;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        roundPlayerPos = new Vector3(Mathf.RoundToInt(player.position.x/16)*16,Mathf.RoundToInt(player.position.y/9)*9,0) + new Vector3(0,0,-10);
        transform.position = Vector3.Lerp(transform.position, roundPlayerPos, camSmooth);
    }
}
