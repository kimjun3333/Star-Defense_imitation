using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveStartUI : MonoBehaviour
{
    [SerializeField] private Button startBtn;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1f, 1f, 1f, 0.3f);

    private void Start()
    {
        startBtn.onClick.AddListener(OnClickStart);

        EventManager.Instance.Subscribe(EventType.WaveEnded, OnWaveEnded);
        EventManager.Instance.Subscribe(EventType.WaveStarted, OnWaveStarted);
    }

    private void OnClickStart()
    {
        StageManager.Instance.StartNextWave();
        SetInteractable(false);
    }

    private void OnWaveStarted(object payload)
    {
        SetInteractable(false);
    }

    private void OnWaveEnded(object payload)
    {
        SetInteractable(true);
    }
    private void SetInteractable(bool value)
    {
        startBtn.interactable = value;

        var img = startBtn.GetComponent<Image>();
        img.color = value ? normalColor : disabledColor;
    }
}
