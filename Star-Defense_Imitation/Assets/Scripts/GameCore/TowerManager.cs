using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 타워 관리를 하는 매니저 클래스
/// </summary>
public class TowerManager : Singleton<TowerManager>
{
    [SerializeField] private List<TowerController> towers = new(); //설치한 타워목록

    public TowerController BuildTower(TowerSO so, Vector3 pos)
    {
        GameObject obj = PoolingManager.Instance.Spawn(
            "Tower",
            DataManager.Instance.GetPrefab("TowerPrefab"),
            pos,
            Quaternion.identity
            );

        TowerController tower = obj.GetComponent<TowerController>();
        tower.Init(so);

        towers.Add(tower);
        return tower;
    }

    public void RemoveTower(TowerController tower)
    {
        towers.Remove(tower);
        PoolingManager.Instance.Despawn("Tower", tower.gameObject);
    }
}
