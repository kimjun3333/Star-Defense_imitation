using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
