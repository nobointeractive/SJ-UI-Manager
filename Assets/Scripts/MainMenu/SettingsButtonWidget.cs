using UnityEngine;

public class SettingsButtonWidget : UIWidget
{
    public void OnSettingsButtonClicked()
    {
        UISceneManager.Instance.ShowPanel("Settings");
    }
}
