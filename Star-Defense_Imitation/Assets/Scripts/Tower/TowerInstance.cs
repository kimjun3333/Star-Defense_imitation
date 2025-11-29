using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstance
{
    public TowerSO Definition;

    public GameObject projectilePrefab; //이 부분도 없앨 예정.

    public float CurrentCooldown;

    public TowerInstance(TowerSO so)
    {
        Definition = so;
        CurrentCooldown = 0f;

        projectilePrefab = DataManager.Instance.GetPrefab(so.ProjectileID);
    }
}
