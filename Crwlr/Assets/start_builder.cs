using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_builder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) other.GetComponent<player_controller>().transitioning = false;
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) other.GetComponent<player_controller>().transitioning = true;
    }
}
