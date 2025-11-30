using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesUI : UIBase
{
    [Header("Resources Text")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI mineralText;

    [Header("Worker Text")]
    [SerializeField] private TextMeshProUGUI workerText;

    public override void OnInit()
    {
        base.OnInit();

        Debug.Log("ResourcesUI OnInit이 호출되었습니다.");
        EventManager.Instance.Subscribe(EventType.ResourceChanged, OnResourceChanged);
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    private void OnResourceChanged(object payload)
    {
        var data = (ResourceChangedPayload)payload;

        if(data.type == ResourceType.Gold)
            goldText.text = $"G : {data.value.ToString()}";
        if(data.type == ResourceType.Mineral)
            mineralText.text = $"M : {data.value.ToString()}";
    }
}
