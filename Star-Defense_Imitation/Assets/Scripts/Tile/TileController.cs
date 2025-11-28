using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Normal,
    Repair,
    Buff
}

public class TileController : MonoBehaviour
{
    [Header("타일 타입 설정")]
    public TileType tileType;

    [Header("설치 여부")]
    public bool hasTower = false;

    public bool CanPlaceTower
    {
        get
        {
            if (tileType == TileType.Repair) return false;

            if (hasTower) return false;

            return true;
        }
    }

    public float GetBuffMult()
    {
        return tileType == TileType.Buff ? 1.3f : 1f;
    }

    public void Repair()
    {
        if(tileType == TileType.Repair)
        {
            tileType = TileType.Normal;
            Debug.Log("타일 타입 -> Normal");
        }
    }

    private void OnMouseDown()
    {
        if(hasTower)
        {
            Debug.Log("이미 타워가 존재합니다.");
            return;
        }

        if(tileType == TileType.Repair)
        {
            Debug.Log("수리타일 입니다. 설치 불가");
            return;
        }

        if(CanPlaceTower)
        {
            //타워건설 가능
        }
    }
}
