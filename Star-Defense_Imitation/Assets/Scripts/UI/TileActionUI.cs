using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileActionUI : UIBase
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject summonPanel;
    [SerializeField] private GameObject repairPanel;
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] private TextMeshProUGUI summonCostText;
    [SerializeField] private TextMeshProUGUI repairCostText;

    private TileController currentTile;

    public override void OnInit()
    {
        base.OnInit();

        summonPanel.SetActive(false);
        repairPanel.SetActive(false);
        background.SetActive(false);
        upgradePanel.SetActive(false);

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

        var upgradeButton = upgradePanel.GetComponent<Button>();
        if (upgradeButton != null)
        {
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(OnClickUpgrade);
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
        screenPos.y += 10f;
        transform.position = screenPos;

        if (tile.tileType == TileType.Repair)
        {
            summonPanel.SetActive(false);
            repairPanel.SetActive(true);
            upgradePanel.SetActive(false);
        }
        else if(tile.hasTower)
        {
            summonPanel.SetActive(false);
            repairPanel.SetActive(false);
            upgradePanel.SetActive(true);
        }
        else
        {
            summonPanel.SetActive(true);
            repairPanel.SetActive(false);
            upgradePanel.SetActive(false);
        }

        summonCostText.text = $"{PlayerManager.Instance.summonCost.ToString()} G";
        repairCostText.text = $"{PlayerManager.Instance.repairCost.ToString()} M";

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
        upgradePanel.SetActive(false);

        currentTile = null;
    }

    public void OnClickRandomSummon()
    {
        if (currentTile == null) return;

        int cost = PlayerManager.Instance.summonCost;

        if (!PlayerManager.Instance.UseResource(ResourceType.Gold, cost))
        {
            Debug.Log("골드 부족");
            return;
        }

        currentTile.TryPlaceRandomTower();
        Close();
    }

    public void OnClickRepair()
    {
        if (currentTile == null) return;

        int cost = PlayerManager.Instance.repairCost;

        if (!PlayerManager.Instance.UseResource(ResourceType.Mineral, cost))
        {
            Debug.Log("미네랄 부족");
            return;
        }

        currentTile.Repair();
        Close();
    }

    public void OnClickUpgrade()
    {
        if (currentTile == null) return;
        if (currentTile.CurrentTower == null) return;

        TowerUpgradeManager.Instance.Upgrade(currentTile.CurrentTower);

        Close();
    }

    public void OnClickBackground()
    {
        Close();
    }
}
