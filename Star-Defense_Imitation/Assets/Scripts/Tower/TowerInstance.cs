using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstance
{
    public TowerSO Definition;

    //강화될 여지가 있음
    public float RuntimeDmg; 
    public float RuntimeAtkSpeed;

    public float CurrentCooldown;

    public TowerInstance(TowerSO so)
    {
        Definition = so;
        CurrentCooldown = 0f;

        RuntimeDmg = so.Dmg;
        RuntimeAtkSpeed = so.AtkSpeed;
    }
}
