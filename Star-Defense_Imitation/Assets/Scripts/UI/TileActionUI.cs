using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileActionUI : UIBase
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject summonPanel;
    [SerializeField] private GameObject repairPanel;

    private TileController currentTile;

    public override void OnInit()
    {
        base.OnInit();

        summonPanel.SetActive(false);
        repairPanel.SetActive(false);
        background.SetActive(false);

        var bgButton = background.GetComponent<Button>();
        if (bgButton != null)
        {
            bgButton.onClick.RemoveAllListeners();  
            bgButton.onClick.AddListener(OnClickBackground);
        }

        var summonButton = summonPanel.GetComponent<Button>();
        if (summonButton != null)
        {
            summonButton.onClick.RemoveAllListeners();
            summonButton.onClick.AddListener(OnClickRandomSummon);
        }

        var repairButton = repairPanel.GetComponent<Button>();
        if (repairButton != null)
        {
            repairButton.onClick.RemoveAllListeners();
            repairButton.onClick.AddListener(OnClickRepair);
        }

        EventManager.Instance.Subscribe(EventType.TileClicked, OnTileClicked);
    }

    private void OnTileClicked(object payload)
    {
        var data = (TileClickedPayload)payload;
        TileController tile = data.tile;

        if (tile == null)
        {
            Close();
            return;
        }

        currentTile = tile;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(tile.transform.position);
        transform.position = screenPos;

        if (tile.tileType == TileType.Repair)
        {
            summonPanel.SetActive(false);
            repairPanel.SetActive(true);
        }
        else
        {
            summonPanel.SetActive(true);
            repairPanel.SetActive(false);
        }

        Open();
    }

    public override void OnOpen()
    {
        background.SetActive(true);
    }

    public override void OnClose()
    {
        background.SetActive(false);
        summonPanel.SetActive(false);
        repairPanel.SetActive(false);

        currentTile = null;
    }

    public void OnClickRandomSummon()
    {
        if (currentTile == null) return;

        currentTile.TryPlaceRandomTower();
        Close();
    }

    public void OnClickRepair()
    {
        if (currentTile == null) return;

        currentTile.Repair();
        Close();
    }

    public void OnClickBackground()
    {
        Close();
    }
}
