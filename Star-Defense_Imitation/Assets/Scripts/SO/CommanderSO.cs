using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CommanderSheetData : BaseSheetData
{
    public float Dmg;
    public float Range;
    public float AtkSpeed;

    public string SkillID;
    public string ProjectileSpriteID;
}
public class CommanderSO : BaseSO
{
    public float Dmg;
    public float Range;
    public float AtkSpeed;

    public string SkillID;
    public string ProjectileSpriteID;

    public Sprite ProjectileSprite;
    public override void ApplyData(object sheetData)
    {
        if (sheetData is not CommanderSheetData data) return;

        ApplyBaseData(data);

        Dmg = data.Dmg;
        Range = data.Range;
        AtkSpeed = data.AtkSpeed;
        SkillID = data.SkillID;
        ProjectileSpriteID = data.ProjectileSpriteID;
    }
}
