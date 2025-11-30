using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour
{
    [Header("기본")]
    [SerializeField] private Image towerSprite;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [Header("능력치")]
    [SerializeField] private TextMeshProUGUI dmgText;
    [SerializeField] private TextMeshProUGUI atkSpeedText;

    [Header("세부사항")]
    [SerializeField] private TextMeshProUGUI targetTypeText;
    [SerializeField] private TextMeshProUGUI tribeTypeText;

    public void SetData(TowerController tower)
    {
        TowerSO data = tower.Instance.Definition;

        towerSprite.sprite = data.Sprite;
        nameText.text = data.Name;

        dmgText.text = $"ATK : {data.Dmg}";
        atkSpeedText.text = $"ATKSpeed : {data.AtkSpeed}";

        targetTypeText.text = $"Target : [{data.TargetType}]";

        tribeTypeText.text = $"Tribe : [{data.Tribe}]";
    }
}
