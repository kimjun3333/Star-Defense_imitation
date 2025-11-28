using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<EnemyController> enemies = new(); //소환된 적 리스트

    public int EnemyCount => enemies.Count;

    public void Register(EnemyController enemy) //적 등록
    {
        enemies.Add(enemy);
    }

    public void Unregister(EnemyController enemy) //적 해제
    {
        enemies.Remove(enemy);
    }

    public bool IsAllEnemyDead() //Wave의 적 모두 처리했는지 체크용
    {
        return enemies.Count == 0;
    }

    public List<EnemyController> GetEnemies() //타워 적 체크용
    {
        return enemies;
    }


}
