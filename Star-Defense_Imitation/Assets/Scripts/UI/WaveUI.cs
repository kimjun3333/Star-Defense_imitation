using TMPro;
using UnityEngine;

public class WaveUI : UIBase
{
    [SerializeField] private TextMeshProUGUI waveText;

    public override void OnInit()
    {
        base.OnInit();
        EventManager.Instance.Subscribe(EventType.WaveStarted, OnWaveStarted);
    }

    private void OnWaveStarted(object payload)
    {
        var data = (WaveStartedPayload)payload;
        waveText.text = $"Wave : {data.currentWave} / {data.totalWave}";
    }
}
