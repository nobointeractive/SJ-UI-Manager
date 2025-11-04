using UnityEngine;

public class StarsButtonController : MonoBehaviour
{
    public void OnButtonClicked()
    {
        if (UISceneManager.Instance.GetCurrentPanelCount() > 0)
        {
            return;
        }
        UISceneManager.Instance.ShowPanel("StarsPanel");
    }
}
