using UnityEngine;

public class SettingsButtonController : MonoBehaviour
{
    public void OnSettingsButtonClicked()
    {
        UISceneManager.Instance.ShowPanel("Settings");
    }
}
