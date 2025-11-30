using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountyPanel : UIBase
{
    [SerializeField] private Transform content; 
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject background;   
    [SerializeField] private BountySO bountySO;

    private List<GameObject> spawnedButtons = new();

    public override void OnInit()
    {
        base.OnInit();

        var list = DataManager.Instance.GetAllDataOfType<BountySO>();
        if (list == null || list.Count == 0)
        {
            Debug.LogError("BountySO가 없습니다.");
            return;
        }

        bountySO = list[0];

        var bgButton = background.GetComponent<Button>();
        if (bgButton != null)
        {
            bgButton.onClick.RemoveAllListeners();
            bgButton.onClick.AddListener(OnClickBackground);
        }

        GenerateButtonsOnce();

        EventManager.Instance.Subscribe(EventType.BountyButtonClicked, OnOpenBountyPanel);
    }

    private void GenerateButtonsOnce()
    {
        if (bountySO == null)
        {
            Debug.LogWarning("BountySO가 설정되지 않음");
            return;
        }

        foreach (string id in bountySO.EnemyIDList)
        {
            EnemySO enemy = DataManager.Instance.GetData<EnemySO>(id);
            if (enemy == null)
            {
                Debug.LogWarning($"EnemySO '{id}' 로드 실패");
                continue;
            }

            GameObject obj = Instantiate(buttonPrefab, content);
            obj.GetComponent<BountySpawnButton>().SetData(enemy);

            spawnedButtons.Add(obj);
        }
    }
    private void OnOpenBountyPanel(object param)
    {
        Open();
    }

    public override void OnOpen()
    {
        content.gameObject.SetActive(true);
        background.SetActive(true);
    }

    public override void OnClose()
    {
        content.gameObject.SetActive(false);
        background.SetActive(false);
    }

    public void OnClickBackground()
    {
        Close();
    }
}
