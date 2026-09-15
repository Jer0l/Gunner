using UnityEngine;

public class RoomDoor : MonoBehaviour
 {
 [SerializeField] private Transform nextRoomPoint;

 private RoomController roomController;
 private bool unlocked;

 public Transform NextRoomPoint => nextRoomPoint;

 public void Initialize(RoomController controller)
 {
 roomController = controller;
 unlocked = false;
 }

 public void Unlock()
 {
 unlocked = true;
 Debug.Log("Puertas Desbloqueadas");
 }

 private void OnTriggerEnter2D(Collider2D other)
 {
 if (!unlocked)
 {
 Debug.Log("Puetas Bloqueada");
 return;
 }
 if (!other.CompareTag("Player"))
 return;

 roomController.UseDoor(this, other.transform);
 }
 }