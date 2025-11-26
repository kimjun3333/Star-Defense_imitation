using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemyPrefab;
    public Path path; //경로
    public EnemySO testEnemy;

    public void SpawnEnemy(EnemySO enemySO)
    {
        EnemyController enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.Init(enemySO, path.Waypoints);
    }

    public void TestSpawn()
    {
        SpawnEnemy(testEnemy);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TestSpawn();
        }
    }
}
