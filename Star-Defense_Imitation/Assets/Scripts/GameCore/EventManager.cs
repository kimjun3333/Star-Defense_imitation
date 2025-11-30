using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    ResourceChanged,
    WaveStarted,
    WaveEnded,
    EnemyDied,
    TileClicked,
    TowerClicked,
    CommanderClicked,
    GameWin,
    GameOver,
    BountyButtonClicked, //현상금 버튼
    WorkerButtonClicked, //탐사정 버튼
    EnhanceButtonClicked, //강화버튼
}
public class EventManager : Singleton<EventManager>
{
    private readonly Dictionary<EventType, Action<object>> eventTable = new();

    /// <summary>
    /// 이벤트 구독
    /// </summary>
    public void Subscribe(EventType type, Action<object> callback)
    {
        if (!eventTable.ContainsKey(type))
            eventTable[type] = null;

        eventTable[type] += callback;
    }

    /// <summary>
    /// 이벤트 해제
    /// </summary>
    public void Unsubscribe(EventType type, Action<object> callback)
    {
        if (eventTable.ContainsKey(type))
            eventTable[type] -= callback;
    }

    /// <summary>
    /// 이벤트 호출
    /// </summary>
    public void Trigger(EventType type, object payload = null)
    {
        if(eventTable.TryGetValue(type, out var action) && action != null)
        {
            action.Invoke(payload);
        }
    }
}
