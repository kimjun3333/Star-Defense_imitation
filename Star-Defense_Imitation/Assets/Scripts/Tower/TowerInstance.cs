using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstance
{
    public TowerSO Definition;
    public Sprite ProjectileSprite;

    public float CurrentCooldown;

    public TowerInstance(TowerSO so)
    {
        Definition = so;
        CurrentCooldown = 0f;

        ProjectileSprite = DataManager.Instance.GetSpriteByID(Definition.ProjectileID);
    }
}
