using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class WaveSheetData : BaseSheetData
{
    public string Spawns;
}

public class WaveSO : BaseSO
{
    public List<SpawnInfo> Enemies = new();

    public override void ApplyData(object sheetData)
    {
        if (sheetData is not WaveSheetData data) return;

        ApplyBaseData(data);

        Enemies.Clear();

        if (string.IsNullOrWhiteSpace(data.Spawns)) return;

        string[] lines = data.Spawns.Split(new[] {'\n', '\r'},
            StringSplitOptions.RemoveEmptyEntries
            );

        foreach(var line in lines)
        {
            var parts = line.Split(':');
            if(parts.Length != 2) continue;

            string enemyID = parts[0].Trim();

            if (!int.TryParse(parts[1].Trim(), out int count)) continue;

            Enemies.Add(new SpawnInfo
            {
                EnemyID = enemyID,
                Count = count
            });
        }
    }
}

[Serializable]
public class SpawnInfo
{
    public string EnemyID;
    public int Count;
}
