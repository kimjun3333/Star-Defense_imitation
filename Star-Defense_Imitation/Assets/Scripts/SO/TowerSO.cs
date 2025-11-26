using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerSheetData : BaseSheetData
{
    public float Dmg;
    public float Range;
    public float AtkSpeed;

    public string AttackType;
    public string Rarity;
    public string TargetType;
    public string Tribe;

    public string SkillID;

}
public class TowerSO : BaseSO
{
    public float Dmg; //공격력
    public float Range; //사거리
    public float AtkSpeed; //공격속도

    public AttackType AttackType; //근거리 or 원거리
    public RarityType Rarity; //등급
    public UnitType TargetType; //대공 , 지공 , 지대공
    public TribeType Tribe; //종족 => 패시브 스킬개념

    public string SkillID; //스킬 보유 => SO생성시 ID로 매핑할 예정

    public override void ApplyData(object sheetData)
    {
        if (sheetData is not TowerSheetData data) return;

        ApplyBaseData(data);

        Dmg = data.Dmg;
        Range = data.Range;
        AtkSpeed = data.AtkSpeed;
        SkillID = data.SkillID;

        if (Enum.TryParse(data.AttackType, true, out AttackType parsedAttackType))
            AttackType = parsedAttackType;
        else
            Debug.LogError($"{data.AttackType}은 AttackType 형식에 맞지 않습니다.");

        if (Enum.TryParse(data.Rarity, true, out RarityType parsedRarityType))
            Rarity = parsedRarityType;
        else
            Debug.LogError($"{data.Rarity}은 RarityType 형식에 맞지 않습니다.");

        if (Enum.TryParse(data.TargetType, true, out UnitType parsedUnitType))
            TargetType = parsedUnitType;
        else
            Debug.LogError($"{data.TargetType}은 UnitType 형식에 맞지 않습니다.");

        if (Enum.TryParse(data.Tribe, true, out TribeType parsedTribeType))
            Tribe = parsedTribeType;
        else
            Debug.LogError($"{data.Tribe}은 TribeType 형식에 맞지 않습니다.");

    }
}
