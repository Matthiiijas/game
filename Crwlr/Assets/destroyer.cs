﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("spawn_point")) Destroy(other.gameObject);
    }
}
