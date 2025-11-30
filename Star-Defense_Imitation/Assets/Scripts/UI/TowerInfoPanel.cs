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
        TowerSO def = tower.Instance.Definition;
        TowerInstance instance = tower.Instance;

        towerSprite.sprite = def.Sprite;
        rarityText.text = def.Rarity.ToString();
        nameText.text = def.Name;

        dmgText.text = $"ATK : {instance.RuntimeDmg}";
        atkSpeedText.text = $"ATKSpeed : {instance.RuntimeAtkSpeed}";

        targetTypeText.text = $"Target : [{def.TargetType}]";

        tribeTypeText.text = $"Tribe : [{def.Tribe}]";
    }
}
