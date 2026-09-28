using ED262C;
using UnityEngine;

public class RoomMapData
{
    public int roomID;
    public Vector2 mapPosition;
    public int enteredThroughDoor;

    public RoomMapData(int roomID, Vector2 mapPosition, int enteredThroughDoor)
    {
        this.roomID = roomID;
        this.mapPosition = mapPosition;
        this.enteredThroughDoor = enteredThroughDoor;
    }
}