using ED262C;
 using System.Collections.Generic;
 using UnityEngine;

public class EnemyFactory : MonoBehaviour
 {
 [SerializeField] private GameObject goblinPrefab;
 [SerializeField] private GameObject archerPrefab;
 [SerializeField] private GameObject knightPrefab;

 private SimpleArrayList<Enemy> enemyList;
 private Dictionary<string, GameObject> enemyDictionary = new Dictionary<string, GameObject>();

 private void Awake()
 {
 enemyList = new SimpleArrayList<Enemy>();
 enemyDictionary.Add("Goblin", goblinPrefab);
 enemyDictionary.Add("Archer", archerPrefab);
 enemyDictionary.Add("Knight", knightPrefab);
 }

 public Enemy CreateEnemy(string enemyType, Vector3 position, Transform enemyContainer)
 {
 if (enemyDictionary.ContainsKey(enemyType))
 {
 Enemy enemy = new Enemy(enemyType);
 enemyList.Add(enemy);

 Instantiate(enemyDictionary[enemyType], position, Quaternion.identity, enemyContainer);

 return enemy;
 }
 else
 {
 Debug.LogWarning("Enemy type not found: " + enemyType);
 return null;
 }
 }

 //private void ShowEnemies()
 //{
 // Debug.Log("=== ENEMIGOS DEL FACTORY ===");

 // for (int i = 0; i < enemyList.Count; i++)
 // {
 // Debug.Log("Enemigo " + i + ": " + enemyList[i]);
 // }

 // Debug.Log("Total de enemigos: " + enemyList.Count);
 //}
 }