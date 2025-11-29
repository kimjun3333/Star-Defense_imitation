using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour, IPoolable
{
    [SerializeField] private TowerInstance instance;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Init(TowerSO so)
    {
        instance = new TowerInstance(so);
        spriteRenderer.sprite = so.Sprite;
    }


    public void OnSpawned()
    {
        instance = null;
        StopAllCoroutines();
    }

    public void OnDespawned()
    {
        instance = null;
        StopAllCoroutines();
    }

    private void Update()
    {
        if (instance == null) return;

        if (instance.CurrentCooldown > 0)
            instance.CurrentCooldown -= Time.deltaTime;

        EnemyController target = FindTarget();
        if (target == null) return;

        if (instance.CurrentCooldown <= 0)
        {
            Attack(target);
            instance.CurrentCooldown = 1f / instance.Definition.AtkSpeed;
        }
    }


    private EnemyController FindTarget()
    {
        List<EnemyController> enemies = EnemyManager.Instance.GetEnemies();

        float range = instance.Definition.Range;

        foreach (var enemy in enemies)
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
        GameObject obj = PoolingManager.Instance.Spawn(
            "Projectile",
            transform.position,
            Quaternion.identity
            );

        ProjectileController projectile = obj.GetComponent<ProjectileController>();
        projectile.Init(enemy, instance.Definition.Dmg, 8f, instance.ProjectileSprite);
    }
}
