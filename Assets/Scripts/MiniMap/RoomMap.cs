using UnityEngine;
using ED262C;

public class RoomMap : MonoBehaviour
{
    private SimpleArraySet<int> discoveredRooms;

    private void Awake()
    {
        discoveredRooms = new SimpleArraySet<int>();
    }

    public void DiscoverRoom(int roomID)
    {
        if (!discoveredRooms.Contains(roomID))
        {
            discoveredRooms.Add(roomID);
        }
    }

    public bool HasDiscoveredRoom(int roomID)
    {
        return discoveredRooms.Contains(roomID);
    }

    public int[] GetDiscoveredRooms()
    {
        return discoveredRooms.ToArray();
    }
}