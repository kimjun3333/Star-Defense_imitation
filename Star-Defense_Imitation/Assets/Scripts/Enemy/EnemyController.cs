using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyController : MonoBehaviour, IPoolable
{
    [SerializeField] private EnemyInstance instance;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private List<Transform> waypoints;
    private int waypointIndex = 0;

    private CommanderController commander;

    [SerializeField] private float attackRange = 0.5f;
    private float currentCooldown = 0f;

    public void Init(EnemySO so, List<Transform> path)
    {
        instance = new EnemyInstance(so);
        spriteRenderer.sprite = so.Sprite;

        waypoints = path;
        waypointIndex = 0;
        transform.position = waypoints[0].position;

        EnemyManager.Instance.Register(this);

        commander = CommanderController.Instance;

        currentCooldown = 0f;
    }
    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnSpawned()
    {
        instance = null;
        waypoints = null;
        waypointIndex = 0;

        commander = null;
        currentCooldown = 0f;

        StopAllCoroutines();
    }

    public void OnDespawned()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.Unregister(this);

        instance = null;
        waypoints = null;
        waypointIndex = 0;

        commander = null;
        currentCooldown = 0f;

        StopAllCoroutines();
    }

    private void Update()
    {
        if (instance == null) return;

        if (commander == null || commander.IsDead())
        {
            Move();
            return;
        }

        float distToCommander = Vector2.Distance(transform.position, commander.transform.position);

        if (distToCommander <= attackRange)
        {
            AttackCommander();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        if (waypointIndex >= waypoints.Count) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[waypointIndex].position,
            instance.CurrentSpeed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Count)
            {
                Debug.Log("골인");

                PoolingManager.Instance.Despawn("Enemy", gameObject);
            }
        }
    }

    private void AttackCommander()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            return;
        }

        commander.TakeDamage(instance.Definition.Dmg);

        currentCooldown = 1f / instance.Definition.AtkSpeed;
    }

    public void TakeDamage(float dmg)
    {
        instance.CurrentHealth -= dmg;

        if (instance.CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerManager.Instance.AddResource(instance.Definition.RewardType, instance.Definition.Reward);

        PoolingManager.Instance.Despawn("Enemy", gameObject);
    }
}
