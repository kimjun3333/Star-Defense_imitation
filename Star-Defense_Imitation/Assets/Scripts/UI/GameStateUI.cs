using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateUI : UIBase
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    public override void OnInit()
    {
        base.OnInit();

        EventManager.Instance.Subscribe(EventType.GameWin, OnGameWin);
        EventManager.Instance.Subscribe(EventType.GameOver, OnGameOver);
    }

    private void OnGameWin(object payload)
    {
        UIManager.Instance.CloseAllUI();
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        Open();
    }

    private void OnGameOver(object payload)
    {
        UIManager.Instance.CloseAllUI();
        winPanel.SetActive(false);
        losePanel.SetActive(true);
        Open();
    }
    public override void OnClose()
    {
        base.OnClose();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
}
