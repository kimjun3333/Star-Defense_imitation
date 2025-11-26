using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// SO의 베이스가 되는 스크립트
/// </summary>
public abstract class BaseSO : ScriptableObject
{
    public string ID;
    public string Name;

    public string SpriteID;
    public Sprite Sprite; 

    public virtual void ApplyBaseData(BaseSheetData baseData)
    {
        if (baseData == null) return;

        ID = baseData.ID;
        Name = baseData.Name;

        SpriteID = baseData.SpriteID;
        Sprite = null;
    }

    public abstract void ApplyData(object sheetData); //런타임 갱신용 매서드
}
