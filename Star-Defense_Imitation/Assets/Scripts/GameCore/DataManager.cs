using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DataManager : Singleton<DataManager>, IInitializable
{
    [SerializeReference] private Dictionary<string, List<Object>> dataByLabel = new();

    public async Task Init()
    {
        foreach (var kvp in AddressableLoader.Instance.loadedData) //로드된 어드레서블 데이터를 DataManager에 추가
        {
            AddData(kvp.Key, kvp.Value);
        }

        Debug.Log("DataManager 준비 완료");
        await Task.CompletedTask;
    }

    /// <summary>
    /// Addressable에서 불러온 데이터 라벨별로 등록
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="label"></param>
    /// <param name="assets"></param>
    public void AddData(string label, IEnumerable<Object> assets)
    {
        if (!dataByLabel.ContainsKey(label))
            dataByLabel[label] = new List<Object>();

        foreach (var asset in assets)
        {
            if (!dataByLabel[label].Contains(asset))
            {
                dataByLabel[label].Add(asset);
            }
        }

        Debug.Log($"DataManager : [{label}] {dataByLabel[label].Count}개 데이터 추가 완료");
    }

    public List<T> GetAllDataOfType<T>() where T : Object
    {
        List<T> result = new();
        foreach (var kvp in dataByLabel)
        {
            foreach (var item in kvp.Value)
            {
                if (item is T tItem)
                    result.Add(tItem);
            }
        }

        return result;
    }
    
    /// <summary>
    /// BaseSO를 상속받는 Data를 ID로 가져오는 함수
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public T GetData<T>(string id) where T : BaseSO
    {
        foreach(var kvp in dataByLabel)
        {
            foreach(var item in kvp.Value)
            {
                if (item is T so && so.ID == id)
                    return so;
            }
        }

        return null;
    }

    /// <summary>
    /// GameObject id로 가져오는 함수
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject GetPrefab(string id)
    {
        var list = GetAllDataOfType<GameObject>();
        return list.Find(x => x.name == id);    
    }

    public List<T> GetAllDataOfTypeByLabel<T>(string label) where T : Object
    {
        List<T> result = new();

        if (!dataByLabel.ContainsKey(label))
            return result;

        foreach(var item in dataByLabel[label])
        {
            if(item is T tItem)
                result.Add(tItem);
        }

        return result;
    }
}
