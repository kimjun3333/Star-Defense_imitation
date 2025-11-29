using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IPoolable
{
    private EnemyController target;
    private float dmg;
    private float speed;

    [SerializeField] private SpriteRenderer spriteRenderer;


    public void Init(EnemyController target, float dmg, float speed, Sprite sprite)
    {
        this.target = target;
        this.dmg = dmg;
        this.speed = speed;

        spriteRenderer.sprite = sprite;
    }
    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnSpawned()
    {
        target = null;
        StopAllCoroutines();
    }

    public void OnDespawned()
    {
        target = null;
        StopAllCoroutines();
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            PoolingManager.Instance.Despawn("Projectile", gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime
            );

        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            HitTarget();
    }

    private void HitTarget()
    {
        target.TakeDamage(dmg);
        PoolingManager.Instance.Despawn("Projectile", gameObject);
    }

}
