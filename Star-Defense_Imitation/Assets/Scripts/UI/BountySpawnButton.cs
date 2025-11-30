using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BountySpawnButton : MonoBehaviour
{
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button button;

    private EnemySO enemyData;
    private void Awake()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    public void SetData(EnemySO so)
    {
        enemyData = so;

        enemyImage.sprite = so.Sprite;
        nameText.text = so.Name;
        rewardText.text = $"{so.RewardType} +{so.Reward}";
    }

    private void OnClicked()
    {
        if (enemyData == null) return;

        var spawner = StageManager.Instance.spawner;

        var path = StageManager.Instance.CurrentPath.Waypoints;

        spawner.SpawnEnemy(enemyData, path);
    }
}
