using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BountySheetData : BaseSheetData
{
    public string EnemyIDs;
}
public class BountySO : BaseSO
{
    public List<string> EnemyIDList = new();

    public override void ApplyData(object sheetData)
    {
        if (sheetData is not BountySheetData data)
            return;

        ApplyBaseData(data);

        EnemyIDList.Clear();

        if (string.IsNullOrWhiteSpace(data.EnemyIDs))
            return;

        string[] lines = data.EnemyIDs.Split(new[] { '\n', '\r' },
            StringSplitOptions.RemoveEmptyEntries
        );

        foreach (var line in lines)
        {
            string id = line.Trim();

            if (!string.IsNullOrWhiteSpace(id))
                EnemyIDList.Add(id);
        }
    }
}
