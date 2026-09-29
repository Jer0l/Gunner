using UnityEngine;
using ED262C;

public enum RoomDirection 
{ 
    Up, Right 
}
public class RoomMap : MonoBehaviour
{
    private SimpleArraySet<int> discoveredRooms;
    private SimpleArraySet<string> roomConnections;

    private void Awake()
    {
        discoveredRooms = new SimpleArraySet<int>();
        roomConnections = new SimpleArraySet<string>();
    }

    public void DiscoverRoom(int roomID)
    {
        if (!discoveredRooms.Contains(roomID))
        {
            discoveredRooms.Add(roomID);
        }
    }
    public void AddConnection(int roomID, RoomDirection direction)
    {
        string connection = roomID + "_" + direction;

        roomConnections.Add(connection);
    }
    public bool HasDiscoveredRoom(int roomID)
    {
        return discoveredRooms.Contains(roomID);
    }
    public bool HasConnection(int roomID, RoomDirection direction)
    {
        string connection = roomID + "_" + direction;

        return roomConnections.Contains(connection);
    }
    public int[] GetDiscoveredRooms()
    {
        return discoveredRooms.ToArray();
    }
}