using TMPro;
using UnityEngine;

public class CommanderInfoPanel : MonoBehaviour
{
    [Header("기본")]
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("능력치")]
    [SerializeField] private TextMeshProUGUI dmgText;
    [SerializeField] private TextMeshProUGUI atkSpeedText;
    [SerializeField] private TextMeshProUGUI healthText;

    public void SetData(CommanderController commander)
    {
        CommanderSO data = commander.Definition;

        nameText.text = data.Name;
        dmgText.text = $"ATK : {data.Dmg}";
        atkSpeedText.text = $"ATKSpeed : {data.AtkSpeed}";
        healthText.text = $"HP : {data.Health}";
    }
}
