using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>, IInitializable
{
    private readonly Dictionary<string, UIBase> activeUI = new();    
    private readonly Dictionary<string, UIBase> allUI = new();       
    private readonly Dictionary<LayerType, Transform> layerRoots = new();
    private readonly Dictionary<string, GameObject> uiPrefabs = new();     // UI 프리팹 캐시

    private Transform uiRoot;

    public async Task Init()
    {
        GameObject rootObj = new GameObject("UIRoot");
        uiRoot = rootObj.transform;

        foreach (LayerType layer in Enum.GetValues(typeof(LayerType)))
            layerRoots[layer] = CreateLayerRoot(layer, uiRoot);

        List<GameObject> uiList = DataManager.Instance.GetAllDataOfTypeByLabel<GameObject>("UI");
        foreach(var prefab in uiList)
        {
            if (prefab == null) continue;

            if(!uiPrefabs.ContainsKey(prefab.name))
                uiPrefabs.Add(prefab.name, prefab);
        }

        foreach(var kvp in uiPrefabs)
        {
            var prefab = kvp.Value;

            var uiBase = prefab.GetComponent<UIBase>();
            if (uiBase == null) continue;

            var instance = Instantiate(prefab, layerRoots[uiBase.layerType]);
            instance.name = prefab.name;

            var ui = instance.GetComponent<UIBase>();
            ui.Init();
            ui.Open();

            allUI[prefab.name] = ui;
        }

        Debug.Log($"UIManager Init 완료 등록 UI : {allUI.Count}개");

        await Task.CompletedTask;
    }

    public T Open<T>(string id = null) where T : UIBase
    {
        string key = id ?? typeof(T).Name;

        if (!allUI.TryGetValue(key, out var ui))
        {
            Debug.LogError($"UIManager: {key} UI가 Init에서 생성되지 않음");
            return null;
        }

        if (!activeUI.ContainsKey(key))
        {
            ui.Open();
            activeUI[key] = ui;
        }

        return (T)ui;
    }

    public void Close<T>(string id = null) where T : UIBase
    {
        string key = id ?? typeof(T).Name;

        if (activeUI.TryGetValue(key, out var ui))
        {
            ui.Close();
            activeUI.Remove(key);
        }
    }

    private Transform CreateLayerRoot(LayerType layerType, Transform parent)
    {
        GameObject root = new($"{layerType}Canvas");
        root.transform.SetParent(parent, false);

        Canvas canvas = root.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = (int)layerType;

        CanvasScaler scaler = root.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.matchWidthOrHeight = 0.5f;

        root.AddComponent<GraphicRaycaster>();

        var safeArea = new GameObject("SafeArea", typeof(RectTransform));
        safeArea.transform.SetParent(root.transform, false);

        RectTransform rt = safeArea.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.one;

        return safeArea.transform;
    }
}
