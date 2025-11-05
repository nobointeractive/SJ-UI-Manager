using UnityEngine;

public class StarsButtonController : MonoBehaviour
{
    public void OnButtonClicked()
    {
        // Only show the StarsPanel if there are no other panels currently displayed
        if (UISceneManager.Instance.GetCurrentPanelCount() > 0)
        {
            return;
        }
        UISceneManager.Instance.ShowPanel("StarsPanel");
    }
}
