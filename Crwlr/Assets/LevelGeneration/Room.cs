using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Room {
    public Vector2 gridPos;
    public roomType type;

    public bool doorTop, doorBot, doorLeft, doorRight;
    public bool doorBossTop, doorBossBot, doorBossLeft, doorBossRight;

    public Room(Vector2 _gridPos, roomType _type) {
        gridPos = _gridPos;
        type= _type;
    }
}
