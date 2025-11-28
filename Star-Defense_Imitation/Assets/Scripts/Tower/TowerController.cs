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

        foreach(var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist <= range)
            {
                return enemy;
            }
        }

        return null;
    }

    private void Attack(EnemyController enemy)
    {
        Debug.Log($"Attack() enemy null? {(enemy == null)}");
        Debug.Log($"{instance.Definition.Name}의 공격시도!");

        if (instance.projectilePrefab == null)
        {
            Debug.LogError($"ProjectilePrefab이 없습니다. ID {instance.Definition.ProjectileID}");
            return;
        }

        GameObject obj = Instantiate(instance.projectilePrefab, transform.position, Quaternion.identity);

        ProjectileController projectile = obj.GetComponent<ProjectileController>();
        projectile.Init(enemy, instance.Definition.Dmg, 8f);
    }
}
