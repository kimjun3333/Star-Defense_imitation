using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(EnemySO enemySO, List<Transform> wayPoints)
    {
        GameObject obj = PoolingManager.Instance.Spawn(
            "Enemy",
            transform.position,
            Quaternion.identity
            );

        EnemyController enemy = obj.GetComponent<EnemyController>();
        enemy.Init(enemySO, wayPoints);
    }
}
