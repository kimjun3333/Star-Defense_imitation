using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


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
        if (tower.ownerTile != null)
        {
            tower.ownerTile.RemoveTower();
            tower.ownerTile = null;
        }

        towers.Remove(tower);
        PoolingManager.Instance.Despawn("Tower", tower.gameObject);
    }

    public List<TowerController> GetSameIDTowers(TowerController tower)
    {
        List<TowerController> list = new();

        foreach (var t in towers)
        {
            if (t.Instance.Definition.ID == tower.Instance.Definition.ID)
                list.Add(t);
        }

        return list;
    }
}
