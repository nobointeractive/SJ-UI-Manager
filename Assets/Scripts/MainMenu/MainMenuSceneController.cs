using UnityEngine;

public class MainMenuSceneController : MonoBehaviour
{
    public GameUIConfiguration uiConfiguration;

    void Awake()
    {
        UISceneManager.Instance.Initialize(uiConfiguration);
    }

    private void Start()
    {
        UISceneManager.Instance.ShowPanel(uiConfiguration.SplashPanelIndex);
    }

    public void ShowMainMenu()
    {
    }

    public void ShowSettings()
    {
    }
}
