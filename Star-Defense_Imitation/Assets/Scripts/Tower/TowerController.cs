using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
   private TowerInstance instance;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Init(TowerSO so)
    {
        instance = new TowerInstance(so);
        spriteRenderer.sprite = so.Sprite;
    }

    private void Update()
    {
        if (instance == null) return;

        if(instance.CurrentCooldown > 0)
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
        List<EnemyController> enemies = EnemyManager.Instance.GetEnemies();

        float range = instance.Definition.Range;
        EnemyController targetEnemy = null;
        float nearDistance = float.MaxValue;

        foreach(var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if(dist <= range && dist < nearDistance)
            {
                nearDistance = dist;
                targetEnemy = enemy;
            }
        }

        return targetEnemy;
    }

    private void Attack(EnemyController enemy)
    {
        float dmg = instance.Definition.Dmg;
        enemy.TakeDamage(dmg);

        Debug.Log($"{instance.Definition.name} 타워가 {enemy.name} 공격! {dmg} 데미지");
    }
}
