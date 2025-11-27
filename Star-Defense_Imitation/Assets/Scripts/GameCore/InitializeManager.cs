using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 초기화 매니저
/// </summary>
public class InitializeManager : Singleton<InitializeManager>
{
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("초기화 시작");
    }
}
