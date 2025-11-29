using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInstance
{
    public TowerSO Definition;

    public float CurrentCooldown;

    public TowerInstance(TowerSO so)
    {
        Definition = so;
        CurrentCooldown = 0f;
    }
}
