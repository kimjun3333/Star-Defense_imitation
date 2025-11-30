using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySheetData : BaseSheetData
{
    public float Health;
    public float Speed;

    public float Dmg;
    public float AtkSpeed;

    public string UnitType;
    public string RewardType;
    public int Reward;
}
[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : BaseSO
{
    public float Health;
    public float Speed;

    public float Dmg;
    public float AtkSpeed;

    public UnitType UnitType;
    public ResourceType RewardType; //골드 Or 미네랄
    public int Reward;

    public override void ApplyData(object sheetData)
    {
        if (sheetData is not EnemySheetData data) return;

        ApplyBaseData(data);

        Health = data.Health;
        Speed = data.Speed;
        Dmg = data.Dmg;
        AtkSpeed = data.AtkSpeed;

        Reward = data.Reward;


        if (Enum.TryParse(data.UnitType, true, out UnitType parsedUnitType))
            UnitType = parsedUnitType;
        else
            Debug.LogError($"{data.UnitType}은 UnitType 형식에 맞지 않습니다.");

        if(Enum.TryParse(data.RewardType, true, out ResourceType parsedRewardType))
            RewardType = parsedRewardType;
        else
            Debug.LogError($"{data.RewardType}은 ResourceType 형식에 맞지 않습니다.");
    }
}
