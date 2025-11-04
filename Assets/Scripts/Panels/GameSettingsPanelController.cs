using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingsPanelController : MonoBehaviour
{
    public void OnButtonClicked()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
}
