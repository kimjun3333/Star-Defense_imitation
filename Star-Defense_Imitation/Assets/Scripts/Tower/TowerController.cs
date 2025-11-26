using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
   private TowerInstance instance;

    public void Init(TowerSO so)
    {
        instance = new TowerInstance(so);
    }

    private void Update()
    {
        if (instance.CurrentCooldown > 0)
            instance.CurrentCooldown -= Time.deltaTime;

        EnemyController target = FindTarget();
        if (target == null) return;

        if(instance.CurrentCooldown <= 0)
        {
            Attack(target);
            instance.CurrentCooldown = 1f / instance.Definition.AtkSpeed;
        }
    }

    private EnemyController FindTarget()
    {
        return null;
    }

    private void Attack(EnemyController enemy)
    {
        
    }
}
