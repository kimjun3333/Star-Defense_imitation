using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private EnemyController target;
    private float dmg;
    private float speed;

    public void Init(EnemyController target, float dmg, float speed)
    {
        this.target = target;
        this.dmg = dmg;
        this.speed = speed;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
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

        Destroy(gameObject); //todo 풀링
    }
}
