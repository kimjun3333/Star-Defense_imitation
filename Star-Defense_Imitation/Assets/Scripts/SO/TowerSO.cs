using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSO : BaseSO
{
    public float Dmg; //공격력
    public float Range; //사거리
    public float AtkSpeed; //공격속도 

    //TODO 아래내용들은 Enum으로 교체할 예정
    public string Rarity; //등급 Enum으로 변경 예정
    public string TargetType; //대상타입 Enum으로 변경 예정
    public string Tribe; //종족 => 패시브 스킬개념
    public string SkillID; //스킬 보유 => SO생성시 ID로 매핑할 예정
}
