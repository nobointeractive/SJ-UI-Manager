using UnityEngine;

public class ClaimRewardsPanelController : MonoBehaviour
{
    [SerializeField] private UIWidget coinRewardWidget;
    [SerializeField] private UIWidget starRewardWidget;

    public void OnCloseButtonClicked()
    {
        GetComponent<UIPanel>().ClosePanel();

        UISceneManager.Instance.PlayFlyer("Coin", coinRewardWidget, UISceneManager.Instance.WidgetLayout.GetWidget("CoinsBar"), 1.0f);
        UISceneManager.Instance.PlayFlyer("Star", starRewardWidget, UISceneManager.Instance.WidgetLayout.GetWidget("StarsBar"), 1.0f);
    }
}
