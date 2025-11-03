using UnityEngine;

public class SettingsPanelController : MonoBehaviour
{
    public void OnButtonClicked()
    {
        UISceneManager.Instance.ShowPanel("Update");
    }
}
