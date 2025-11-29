using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingManager : Singleton<PoolingManager>, IInitializable
{
    private Dictionary<string, Queue<GameObject>> poolDict = new();
    private Dictionary<string, Transform> parentDict = new();
    private Dictionary<string, GameObject> prefabDict = new();

    public async Task Init()
    {
        var enemyPrefabs = DataManager.Instance.GetAllDataOfTypeByLabel<GameObject>("EnemyPrefab");
        if (enemyPrefabs.Count > 0)
            Preload("Enemy", enemyPrefabs[0], 30);

        var towerPrefabs = DataManager.Instance.GetAllDataOfTypeByLabel<GameObject>("TowerPrefab");
        if (towerPrefabs.Count > 0)
            Preload("Tower", towerPrefabs[0], 30);

        var projectilePrefabs = DataManager.Instance.GetAllDataOfTypeByLabel<GameObject>("ProjectilePrefab");
        if (projectilePrefabs.Count > 0)
            Preload("Projectile", projectilePrefabs[0], 10);

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

        prefabDict[key] = prefab;

        Transform parent = GetParent(key);

        for(int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            poolDict[key].Enqueue(obj);
        }
    }

    public GameObject Spawn(string key, Vector3 pos, Quaternion rot)
    {
        if(!poolDict.ContainsKey(key) || poolDict[key].Count == 0)
        {
            if (!prefabDict.ContainsKey(key))
            {
                Debug.LogError($"{key} Prefab이 PoolingManager에 등록되지 않음 Preload 확인");
                return null;
            }

            Transform parent = GetParent(key);

            for(int i = 0; i < 5; i++) //풀링이 더 필요한 경우 5개씩 추가생성
            {
                GameObject extra = Instantiate(prefabDict[key], parent);
                extra.SetActive(false);
                poolDict[key].Enqueue(extra);
            }
        }

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
