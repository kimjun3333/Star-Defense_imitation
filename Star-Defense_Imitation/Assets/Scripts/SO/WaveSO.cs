using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO",menuName = "SO/WaveSO")]

public class WaveSO : BaseSO
{
    public List<SpawnInfo> Enemies;

    public override void ApplyData(object sheetData)
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class SpawnInfo
{
    public string EnemyID;
    public int Count;
}
