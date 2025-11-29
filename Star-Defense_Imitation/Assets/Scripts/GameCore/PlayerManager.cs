using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>, IInitializable
{
    private Dictionary<ResourceType, int> resources = new();

    public int Life { get; private set; }
    public async Task Init()
    {
        resources[ResourceType.Gold] = 100;
        resources[ResourceType.Mineral] = 0;

        Life = 3;

        await Task.Yield();
    }

    public bool UseResource(ResourceType type, int amount)
    {
        if (resources[type] < amount)
            return false;

        resources[type] -= amount;
        return true;
    }

    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;
    }

    public int GetResource(ResourceType type)
    {
        return resources[type];
    }

    public void LoseLife(int amount)
    {
        Life -= amount;

        if (Life <= 0)
            Debug.Log("GameOver");
    }
}
