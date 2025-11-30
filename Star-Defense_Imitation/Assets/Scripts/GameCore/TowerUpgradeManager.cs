using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeManager : Singleton<TowerUpgradeManager>
{
    public void Upgrade(TowerController baseTower)
    {
        var sameList = TowerManager.Instance.GetSameIDTowers(baseTower);

        if(sameList.Count < 2) return;

        TowerController towerA = baseTower;
        TowerController towerB = sameList.Find(t => t != baseTower);

        TileController targetTile = towerA.ownerTile;
        Vector3 spawnPos = baseTower.transform.position;

        TowerSO nextSO = GetRandomNextGradeTower(baseTower.Instance.Definition);

        if (nextSO == null)
        {
            Debug.LogWarning("다음 등급 타워가 없습니다");
            return;
        }

        TowerManager.Instance.RemoveTower(towerA);
        TowerManager.Instance.RemoveTower(towerB);

        TowerController newTower = TowerManager.Instance.BuildTower(nextSO, spawnPos);

        newTower.ownerTile = targetTile;
        targetTile.SetTower(newTower);
    }

    private TowerSO GetRandomNextGradeTower(TowerSO current)
    {
        RarityType next = current.Rarity + 1;

        List<TowerSO> candidates = new();

        foreach (var so in DataManager.Instance.GetAllDataOfType<TowerSO>())
        {
            if (so.Rarity == next)
                candidates.Add(so);
        }

        if (candidates.Count == 0)
            return null;

        Debug.Log(candidates.Count);

        return candidates[Random.Range(0, candidates.Count)];
    }

}
