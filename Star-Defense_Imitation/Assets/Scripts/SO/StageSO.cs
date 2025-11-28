using System;
using System.Collections.Generic;

[Serializable]
public class StageSheetData : BaseSheetData
{
    public string MapPrefabID;
    public string WaveIDs;

}
public class StageSO : BaseSO
{
    public string MapPrefabID; //스테이지 맵
    public List<string> WaveIDs;

    public override void ApplyData(object sheetData)
    {
        if (sheetData is not StageSheetData data) return;

        ApplyBaseData(data);

        MapPrefabID = data.MapPrefabID;

        WaveIDs = new List<string>(
            data.WaveIDs.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
    }
}
