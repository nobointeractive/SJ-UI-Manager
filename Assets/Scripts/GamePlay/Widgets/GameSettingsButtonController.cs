using UnityEngine;

public class GameSettingsButtonController : MonoBehaviour
{
    public void OnSettingsButtonClicked()
    {
        UISceneManager.Instance.ShowPanel("GameSettingsPanel");
    }
}
