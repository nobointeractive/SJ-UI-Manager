using System.Collections.Generic;
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
        UISceneManager.Instance.ShowPanel(uiConfiguration.SplashPanelIndex, new Dictionary<string, object>
        {
            { "OnComplete", new System.Action(ShowMainMenu) }
        });        
    }

    public void ShowMainMenu()
    {
        UISceneManager.Instance.ShowPanel(uiConfiguration.SettingsPanelIndex);
    }

    public void ShowSettings()
    {
        UISceneManager.Instance.ShowPanel(uiConfiguration.SettingsPanelIndex);
    }
}
