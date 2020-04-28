using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum roomType {
    Start, Empty, Chest, Enemy, Boss,
}

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
