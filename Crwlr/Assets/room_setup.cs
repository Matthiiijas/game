using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction {
    West, North, East, South,
}

public class room_setup : MonoBehaviour
{
    GameObject doorLeft, doorRight, doorBottom, doorTop;

    void Start() {
        doorLeft = transform.Find("DoorLeft").gameObject;
        doorRight = transform.Find("DoorRight").gameObject;
        doorBottom = transform.Find("DoorBottom").gameObject;
        doorTop = transform.Find("DoorTop").gameObject;
    }

    void Update() {
        if(Input.GetKey("a")) CloseDoor(Direction.West);
        if(Input.GetKey("d")) CloseDoor(Direction.East);
        if(Input.GetKey("s")) CloseDoor(Direction.South);
        if(Input.GetKey("w")) CloseDoor(Direction.North);
    }

    void CloseDoor(Direction direction) {
        switch(direction) {
            case Direction.West:
                doorLeft.GetComponent<Animator>().SetTrigger("Close");
                break;
            case Direction.East:
                doorRight.GetComponent<Animator>().SetTrigger("Close");
                break;
            case Direction.South:
                doorBottom.GetComponent<Animator>().SetTrigger("Close");
                break;
            case Direction.North:
                doorTop.GetComponent<Animator>().SetTrigger("Close");
                break;
        }
    }
}
