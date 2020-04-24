using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Preferences for generation")]

    [Tooltip("World size in rooms")]
    public Vector2 worldSize = new Vector2(8,8);
    int gridSizeX, gridSizeY;

    [Tooltip("How many rooms to generate")]
    [Range(0,50)]
    public int numberOfRooms = 20;

    [Space(10)]

    [Tooltip("Prefab to use for standard room")]
    public GameObject roomPrefab;
    [Tooltip("Probability to spawn enemies in a room")]
    public float enemySpawnrate = 0.8f;
    [Tooltip("Probability for rooms to clump together (descending)")]
    public float startProb = 0.2f, endProb = 0.01f;
    float currentProb = 0.2f;
    float percentageOfRooms;

    Room[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();

    roomType currentType;



    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(Vector3.zero, Vector3.Scale((Vector3) worldSize - new Vector3(1,1,0), new Vector3(16,9,0)));
    }

    void Start() {
        if(numberOfRooms >= worldSize.x * worldSize.y) {
            numberOfRooms = Mathf.RoundToInt(worldSize.x * worldSize.y);
            Debug.LogError("Too many rooms for worldsize, set Number of rooms to " + numberOfRooms);
        }
        gridSizeX = (int) worldSize.x;
        gridSizeY = (int) worldSize.y;

        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    void CreateRooms() {
        //Set rooms array size
        rooms = new Room[gridSizeX, gridSizeY];
        //create first room at center
        rooms[gridSizeX/2, gridSizeY/2] = new Room(Vector2.zero, roomType.Start);
        takenPositions.Insert(0,Vector2.zero);

        //Loop for every room to spawn
        Vector2 checkPos = Vector2.zero;
        for(int i = 0; i < numberOfRooms; i++) {
            //Set probability of rooms clumping relative to how many rooms already exist
            float percentageOfRooms = (i + 1) / numberOfRooms;
            currentProb = Mathf.Lerp(startProb, endProb, percentageOfRooms);
            //Grab random valid position
            checkPos = NewPosition();
            //Choose type of room
            if(i < numberOfRooms-1) {
                if(Random.value < enemySpawnrate) currentType = roomType.Enemy;
                else currentType = roomType.Empty;
            }
            else currentType = roomType.Boss;
            //Create this room with determined position and type
            rooms[(int) checkPos.x + gridSizeX/2, (int) checkPos.y + gridSizeY/2] = new Room(checkPos,currentType);
            takenPositions.Insert(0,checkPos);
        }
    }
    //Calculate a valid new position
    Vector2 NewPosition() {
        //Define dummy coordinates, iterations and return vector
        int x = 0, y = 0;
        int iterations = 0;
        Vector2 checkingPos = Vector2.zero;

        do {
            //Choose a random room's position
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int) takenPositions[index].x;
            y = (int) takenPositions[index].y;
            //Go in one of four directions
            bool UpDown = Random.value < 0.5f;
            bool positive = Random.value < 0.5f;
            if(UpDown) {
                if(positive) y++;
                else y--;
            }
            else {
                if(positive) x++;
                else x--;
            }
            //Save result and count iteration
            checkingPos = new Vector2(x,y);
            iterations++;
        }
        //If result's position already taken, out of bounds or clumping prevention is triggered, choose new position
        while(takenPositions.Contains(checkingPos) || x >= gridSizeX/2 || x <= -gridSizeX/2 || y >= gridSizeY/2 || y <= -gridSizeY/2 || NumberOfNeighbours(checkingPos, takenPositions) > 1 && Random.value > currentProb);
        //Return new position
        return checkingPos;
    }
    //Count number of neighbour rooms
    int NumberOfNeighbours(Vector2 checkingPos, List<Vector2> usedPositions) {
        //Define return variable
        int num = 0;
        //If position next to given position is taken, count one up
        if(usedPositions.Contains(checkingPos + Vector2.left)) num++;
        if(usedPositions.Contains(checkingPos + Vector2.right)) num++;
        if(usedPositions.Contains(checkingPos + Vector2.down)) num++;
        if(usedPositions.Contains(checkingPos + Vector2.up)) num++;
        //Return number of neighbours
        return num;
    }
    //Calculate on which side of a room should be a door
    void SetRoomDoors() {
        //Loop through every position in rooms array
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                //Skip if there is no room at x and y
                if(rooms[x,y] == null) continue;
                //Check neighbour below
                if(y-1 < 0) rooms[x,y].doorBot = false;                 //If at border, no door
                else rooms[x,y].doorBot = rooms[x,y-1] != null;         //else set door if neighbour exists
                //Check neighbour above
                if(y+1 >= gridSizeY) rooms[x,y].doorTop = false;
                else rooms[x,y].doorTop = rooms[x,y+1] != null;
                //Check neighbour left
                if(x-1 < 0) rooms[x,y].doorLeft = false;
                else rooms[x,y].doorLeft = rooms[x-1,y] != null;
                //Check neighbour right
                if(x+1 >= gridSizeX) rooms[x,y].doorRight = false;
                else rooms[x,y].doorRight = rooms[x+1,y] != null;
            }
        }
    }
    //Instantiate room objects at calculated positions
    void DrawMap() {
        //Loop through every room in rooms array
        foreach(Room room in rooms) {
            //Skip if empty
            if(room == null) continue;
            //Get room's position
            Vector2 drawPos = room.gridPos;
            //Scale indeces to real worldsize (room dimensions)
            drawPos.x *= 16;
            drawPos.y *= 9;
            //Instantiate given prefab and refer its room class
            GameObject instRoom = Instantiate(roomPrefab, (Vector3) drawPos, Quaternion.identity);
            instRoom.transform.parent = gameObject.transform;
            instRoom.GetComponent<RoomBuilder>().refRoom = room;
        }
    }
}
