using ED262C;
 using UnityEngine;

public class EnemySpawner : MonoBehaviour
 {
 [SerializeField] private EnemyFactory enemyFactory;
 [SerializeField] private Transform enemyContainer;
 [SerializeField] private int numberOfEnemies = 5;

 private Camera mainCamera;

 private void Start()
 {
 mainCamera = Camera.main;
 SpawnEnemies();
 }

 private void SpawnEnemies()
 {
 for (int i = 0; i < numberOfEnemies; i++)
 {
 string enemyType = GetRandomEnemyType();
 Vector3 spawnPosition = GetRandomSpawnPosition();

 Debug.Log("Spawning enemy: " + enemyType + " at " + spawnPosition);

 enemyFactory.CreateEnemy(enemyType, spawnPosition, enemyContainer);
 }
 }

 private string GetRandomEnemyType()
 {
 int randomNumber = Random.Range(0, 3);

 if (randomNumber == 0)
 {
 return "Goblin";
 }
 else if (randomNumber == 1)
 {
 return "Archer";
 }
 else
 {
 return "Knight";
 }
 }

 private Vector3 GetRandomSpawnPosition()
 {
 float cameraHeight = mainCamera.orthographicSize;
 float cameraWidth = cameraHeight * mainCamera.aspect;

 float x = Random.Range(
 mainCamera.transform.position.x - cameraWidth,
 mainCamera.transform.position.x + cameraWidth
 );

 float y = Random.Range(
 mainCamera.transform.position.y - cameraHeight,
 mainCamera.transform.position.y + cameraHeight
 );

 return new Vector3(x, y, 0f);
 }
 }