using UnityEngine;
 using ED262C;

public class RoomGenerator : MonoBehaviour
 {
 [Header("Rooms")]
 [SerializeField] private GameObject initialRoom;
 [SerializeField] private GameObject[] roomPrefabs;
 [SerializeField] private int maxRooms = 5;

 private SimpleArrayQueue<RoomData> roomQueue;
 private RoomController currentRoom;

 private int generatedRooms;

 private void Start()
 {
 roomQueue = new SimpleArrayQueue<RoomData>();

 GenerateRoomQueue();

 SpawnInitialRoom();
 }

 private void GenerateRoomQueue()
 {
 int roomsToQueue = Mathf.Max(0, maxRooms - 1);

 for (int i = 0; i < roomsToQueue; i++)
 {
 if (roomPrefabs.Length == 0)
 return;

 GameObject selectedPrefab = roomPrefabs[i % roomPrefabs.Length];

 roomQueue.Enqueue(new RoomData(selectedPrefab));
 }
 }

 private void SpawnInitialRoom()
 {
 if (initialRoom == null)
 {
 Debug.LogError("No se asigno Initial Room.");
 return;
 }

 GameObject room = Instantiate(initialRoom, Vector2.zero, Quaternion.identity);

 currentRoom = room.GetComponent<RoomController>();

 if (currentRoom == null)
 {
 Debug.LogError("La sala inicial no tiene RoomController.");
 return;
 }

 currentRoom.Initialize(this);

 generatedRooms = 1;
 }

 public void GenerateNextRoom(RoomDoor door, Transform player)
 {
 if (generatedRooms >= maxRooms)
 {
 Debug.Log("Se alcanzo el maximo de salas.");
 return;
 }

 if (roomQueue.IsEmpty)
 {
 Debug.Log("No quedan salas en la Queue.");
 return;
 }

 RoomData roomData = roomQueue.Dequeue();

 Transform nextRoomPoint = door.NextRoomPoint;

 if (nextRoomPoint == null)
 {
 Debug.LogError("La puerta no tiene Next Room Point.");
 return;
 }

 GameObject oldRoom = currentRoom.gameObject;

 GameObject newRoom = Instantiate(
 roomData.roomPrefab,
 roomData.roomPrefab.transform.position,
 roomData.roomPrefab.transform.rotation
 );

 RoomController newRoomController = newRoom.GetComponent<RoomController>();

 if (newRoomController == null)
 {
 Debug.LogError("El prefab de la sala no tiene RoomController.");
 Destroy(newRoom);
 return;
 }

 newRoomController.Initialize(this);

 currentRoom = newRoomController;

 generatedRooms++;

 player.position = newRoom.transform.position;

 Destroy(oldRoom);
 }
 }