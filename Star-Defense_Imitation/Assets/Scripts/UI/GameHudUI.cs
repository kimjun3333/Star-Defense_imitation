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
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
