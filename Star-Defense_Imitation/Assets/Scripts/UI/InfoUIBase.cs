using UnityEngine;
using UnityEngine.UI;

public class InfoUIBase : UIBase
{
    [SerializeField] private GameObject background;
    [SerializeField] private TowerInfoPanel towerPanel;
    [SerializeField] private CommanderInfoPanel commanderPanel;

    public override void OnInit()
    {
        base.OnInit();

        background.SetActive(false);
        towerPanel.gameObject.SetActive(false);
        commanderPanel.gameObject.SetActive(false);

        var bgBtn = background.GetComponent<Button>();
        if (bgBtn != null)
        {
            bgBtn.onClick.RemoveAllListeners();
            bgBtn.onClick.AddListener(Close);
        }

        EventManager.Instance.Subscribe(EventType.TowerClicked, OnTowerClicked);
        EventManager.Instance.Subscribe(EventType.CommanderClicked, OnCommanderClicked);
    }

    private void OnTowerClicked(object payload)
    {
        TowerController tower = payload as TowerController;
        if (tower == null)
        {
            Close();
            return;
        }

        commanderPanel.gameObject.SetActive(false);
        background.SetActive(true);
        towerPanel.gameObject.SetActive(true);
        towerPanel.SetData(tower);

        Open();
    }

    private void OnCommanderClicked(object payload)
    {
        CommanderController commander = payload as CommanderController;
        if (commander == null)
        {
            Close();
            return;
        }

        towerPanel.gameObject.SetActive(false);
        background.SetActive(true);
        commanderPanel.gameObject.SetActive(true);
        commanderPanel.SetData(commander);

        Open();
    }
    public override void OnClose()
    {
        base.OnClose();

        background.SetActive(false);
        towerPanel.gameObject.SetActive(false);
        commanderPanel.gameObject.SetActive(false);
    }
}
