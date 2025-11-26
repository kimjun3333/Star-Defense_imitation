using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class SheetDataList<T>
{
    public List<T> list;
}
public class GoogleSheetLoader
{
    public static async Task<List<T>> LoadSheetData<T>(string url)
    {
        using UnityWebRequest req = UnityWebRequest.Get(url);
        var op = req.SendWebRequest();

        while (!op.isDone)
        {
            await Task.Yield();
        }

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"GoogleSheetLoader : 요청 실패: {req.error}");
            return null;
        }

        string rawJson = req.downloadHandler.text;

        Debug.Log($"[RAW JSON - {typeof(T).Name}] {rawJson}"); //Debug

        string wrappedJson = "{\"list\":" + rawJson + "}";
        SheetDataList<T> wrapper = JsonUtility.FromJson<SheetDataList<T>>(wrappedJson);

        Debug.Log($"GoogleSheetLoader : 데이터 로드 완료 {wrapper.list.Count}개");
        return wrapper.list;
    }
}
