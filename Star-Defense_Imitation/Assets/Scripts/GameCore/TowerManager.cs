using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// 타워 관리를 하는 매니저 클래스
/// </summary>
public class TowerManager : Singleton<TowerManager>, IInitializable
{
    private GameObject towerPrefab; // 시작할때 등록하고

    [SerializeField] private List<TowerController> towers = new(); //설치한 타워목록
    public async Task Init()
    {
        towerPrefab = DataManager.Instance.GetPrefab("TowerPrefab");

        if(towerPrefab == null)
        {
            Debug.LogError("TowerManager : TowerPrefab을 찾을수 없음");     
        }
        else
        {
            Debug.Log("TowerManager : TowerPrefab 로드 완료");
        }

        await Task.Yield();
    }

    public TowerController BuildTower(TowerSO so, Vector3 pos)
    {
        GameObject obj = Instantiate(towerPrefab, pos, Quaternion.identity);

        TowerController tower = obj.GetComponent<TowerController>();
        tower.Init(so);

        towers.Add(tower);
        return tower;
    }

    public void RemoveTower(TowerController tower)
    {
        towers.Remove(tower);
        Destroy(tower.gameObject);
    }
}
