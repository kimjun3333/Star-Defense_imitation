using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHudUI : UIBase
{
    [Header("Resources Text")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI mineralText;

    [Header("Wave Text")]
    [SerializeField] private TextMeshProUGUI waveText;

    public override void OnInit()
    {
        base.OnInit();

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
            goldText.text = data.value.ToString();
        if(data.type == ResourceType.Mineral)
            mineralText.text = data.value.ToString();

    }
}
