using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemyPrefab;

    public void SpawnEnemy(EnemySO enemySO, List<Transform> wayPoints)
    {
        EnemyController enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.Init(enemySO, wayPoints);
    }
}
