using UnityEngine;

public class RoomController : MonoBehaviour
 {
 [SerializeField] private Transform enemyContainer;
 [SerializeField] private RoomDoor doorA;
 [SerializeField] private RoomDoor doorB;

 private RoomGenerator roomGenerator;
 private bool roomCompleted;

 public void Initialize(RoomGenerator generator)
 {
 roomGenerator = generator;
 Debug.Log("Sala creada");
 doorA.Initialize(this);
 doorB.Initialize(this);

 roomCompleted = false;
 }

 private void Update()
 {
 if (roomCompleted)
 {
 return;
 }

 if (AreAllEnemiesDead())
 {
 CompleteRoom();
 }
 }

 private bool AreAllEnemiesDead()
 {
 if (enemyContainer == null)
 return true;

 Transform[] allChildren = enemyContainer.GetComponentsInChildren<Transform>();

 foreach (Transform enemy in allChildren)
 {
 if (enemy == enemyContainer) continue;

 if (enemy.gameObject.activeInHierarchy)
 {
 return false;
 }
 }
 return true;
 }

 private void CompleteRoom()
 {
 roomCompleted = true;
 Debug.Log("Sala Completada");
 doorA.Unlock();
 doorB.Unlock();
 }

 public void UseDoor(RoomDoor door, Transform player)
 {
 roomGenerator.GenerateNextRoom(door, player);
 }
 }