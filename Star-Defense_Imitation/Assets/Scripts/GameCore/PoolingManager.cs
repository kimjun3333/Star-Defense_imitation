using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingManager : Singleton<PoolingManager>, IInitializable
{
    private Dictionary<string, Queue<GameObject>> poolDict = new();
    private Dictionary<string, Transform> parentDict = new();

    public async Task Init()
    {
        GameObject enemyPrefab = DataManager.Instance.GetPrefab("EnemyPrefab");
        Preload("Enemy", enemyPrefab, 30);

        GameObject towerPrefab = DataManager.Instance.GetPrefab("TowerPrefab");
        Preload("Tower", towerPrefab, 30);

        GameObject projectilePrefab = DataManager.Instance.GetPrefab("ProjectilePrefab");
        Preload("Projectile", projectilePrefab, 100);

        await Task.Yield();
    }

    private Transform GetParent(string key)
    {
        if(parentDict.ContainsKey(key))
            return parentDict[key];

        GameObject obj = new GameObject($"Pool_{key}");
        obj.transform.SetParent(transform);
        parentDict[key] = obj.transform;
        return obj.transform;
    }

    public void Preload(string key, GameObject prefab, int count)
    {
        if (!poolDict.ContainsKey(key))
            poolDict[key] = new Queue<GameObject>();

        Transform parent = GetParent(key);

        for(int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            poolDict[key].Enqueue(obj);
        }
    }

    public GameObject Spawn(string key, GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if(!poolDict.ContainsKey(key) || poolDict[key].Count == 0)
            Preload(key, prefab, 1);

        GameObject obj = poolDict[key].Dequeue();
        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(true);

        if (obj.TryGetComponent<IPoolable>(out var rec))
            rec.OnSpawned();

        return obj;
    }


    public void Despawn(string key, GameObject obj)
    {
        if (obj.TryGetComponent<IPoolable>(out var rec))
            rec.OnDespawned();

        obj.SetActive(false);
        poolDict[key].Enqueue(obj);
    }

    
}
