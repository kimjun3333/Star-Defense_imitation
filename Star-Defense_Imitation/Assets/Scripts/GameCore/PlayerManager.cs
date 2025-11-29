using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>, IInitializable
{
    private Dictionary<ResourceType, int> resources = new();

    public async Task Init()
    {
        resources[ResourceType.Gold] = 100;
        resources[ResourceType.Mineral] = 0;

        EventManager.Instance.Trigger(EventType.ResourceChanged,
        new ResourceChangedPayload(ResourceType.Gold, resources[ResourceType.Gold]));

        EventManager.Instance.Trigger(EventType.ResourceChanged,
            new ResourceChangedPayload(ResourceType.Mineral, resources[ResourceType.Mineral]));

        await Task.Yield();
    }

    public bool UseResource(ResourceType type, int amount)
    {
        if (resources[type] < amount)
            return false;

        resources[type] -= amount;

        EventManager.Instance.Trigger(
            EventType.ResourceChanged, 
            new ResourceChangedPayload(type, resources[type])
            );

        return true;
    }

    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;

        EventManager.Instance.Trigger(
            EventType.ResourceChanged,
            new ResourceChangedPayload(type, resources[type])
            );
    }
}
