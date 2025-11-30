using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderController : Singleton<CommanderController>
{
    [SerializeField] private CommanderSO so;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float currentHP;
    private float currentCooldown;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.sprite = so.Sprite;

        currentHP = so.Health;
        currentCooldown = 0f;
    }

    private void Update()
    {
        if (currentHP <= 0)
            return;

        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;

        EnemyController target = FindTarget();
        if (target == null) return;

        if (currentCooldown <= 0)
        {
            Attack(target);
            currentCooldown = 1f / so.AtkSpeed;
        }
    }

    private EnemyController FindTarget()
    {
        List<EnemyController> enemies = EnemyManager.Instance.GetEnemies();
        float range = so.Range;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist <= range)
                return enemy;
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

        projectile.Init(enemy, so.Dmg, 8f, so.ProjectileSprite);
    }

    public void TakeDamage(float dmg)
    {
        if (currentHP <= 0) return;

        currentHP -= dmg;

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("게임 오버");
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }
}
