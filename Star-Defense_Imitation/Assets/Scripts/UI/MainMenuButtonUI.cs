using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonUI : UIBase
{
    [SerializeField] private Button bountyBtn;
    [SerializeField] private Button workerBtn;
    [SerializeField] private Button enhanceBtn;

    public override void OnInit()
    {
        base.OnInit();

        bountyBtn.onClick.RemoveAllListeners();
        workerBtn.onClick.RemoveAllListeners();
        enhanceBtn.onClick.RemoveAllListeners();

        bountyBtn.onClick.AddListener(OnClickedBountyBtn);
        workerBtn.onClick.AddListener(OnClickedWorkerBtn);
        enhanceBtn.onClick.AddListener(OnClickedEnhanceBtn);
    }

    private void OnClickedBountyBtn()
    {
        EventManager.Instance.Trigger(EventType.BountyButtonClicked, null);
    }
    private void OnClickedWorkerBtn()
    {
        EventManager.Instance.Trigger(EventType.WorkerButtonClicked, null);
    }
    private void OnClickedEnhanceBtn()
    {
        EventManager.Instance.Trigger(EventType.EnhanceButtonClicked, null);
    }
}
