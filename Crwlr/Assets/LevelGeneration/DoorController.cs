using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Direction doorDirection;

    public float transitionDistance;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            switch(doorDirection) {
                case Direction.West: other.transform.position += new Vector3(-transitionDistance, 0, 0); break;
                case Direction.East: other.transform.position += new Vector3(transitionDistance, 0, 0); break;
                case Direction.South: other.transform.position += new Vector3(0, -transitionDistance, 0); break;
                case Direction.North: other.transform.position += new Vector3(0, transitionDistance, 0); break;
            }
        }
    }
}
