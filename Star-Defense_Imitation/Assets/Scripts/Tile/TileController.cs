using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Normal,
    Repair,
    Buff
}

public class TileController : MonoBehaviour
{
    [Header("타일 타입 설정")]
    public TileType tileType;

    [Header("설치 여부")]
    public bool hasTower = false;

    public bool CanPlaceTower
    {
        get
        {
            if (tileType == TileType.Repair) return false;

            if (hasTower) return false;

            return true;
        }
    }

    public float GetBuffMult()
    {
        return tileType == TileType.Buff ? 1.3f : 1f;
    }

    public void Repair()
    {
        if(tileType == TileType.Repair)
        {
            tileType = TileType.Normal;
            Debug.Log("타일 타입 -> Normal");
        }
    }

    private void OnMouseDown()
    {
        if(hasTower)
        {
            Debug.Log("이미 타워가 존재합니다.");
            return;
        }

        if(tileType == TileType.Repair)
        {
            Debug.Log("수리타일 입니다. 설치 불가");
            return;
        }

        if(CanPlaceTower)
        {
            //타워건설 가능
        }

        TryPlaceRandomTower();
    }

    private void TryPlaceRandomTower()
    {
        if(!CanPlaceTower)
        {
            Debug.Log("설치 불가능한 타일");
            return;
        }

        TowerSO so = GetRandomTowerSO();
        if(so == null)
        {
            Debug.LogError("타워 뽑기 실패");
            return;
        }

        TowerController tower = TowerManager.Instance.BuildTower(so, transform.position);

        hasTower = true;

        Debug.Log($"타워 설치 완료 : {so.ID}");
    }

    private TowerSO GetRandomTowerSO()
    {
        List<TowerSO> towers = DataManager.Instance.GetAllDataOfType<TowerSO>();
        if (towers == null || towers.Count == 0)
            return null;

        int idx = Random.Range(0, towers.Count);
        return towers[idx];
    }
}
