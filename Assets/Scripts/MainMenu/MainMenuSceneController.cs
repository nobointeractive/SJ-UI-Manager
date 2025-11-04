using System.Collections.Generic;
using UnityEngine;

public enum UIWidgetLayoutState
{
    HideAll = 1,
    ShowAll = 2,
    CoinsBarOnly = 4,
    StarsBarOnly = 8
}

public class MainMenuSceneController : MonoBehaviour
{
    public UIConfiguration uiConfiguration;
    public UIWidgetController widgetLayout;

    void Awake()
    {
        UISceneManager.Instance.Initialize(uiConfiguration, widgetLayout);
        UISceneManager.Instance.SetLayoutState((int)UIWidgetLayoutState.HideAll);
        UISceneManager.Instance.SetWidgetLayoutVisibility(false);
    }

    private void Start()
    {
        UISceneManager.Instance.ShowPanel("SplashPanel", new Dictionary<string, object>
        {
            { "OnComplete", new System.Action(ShowMainMenu) }
        });
    }

    public void ShowMainMenu()
    {
        UISceneManager.Instance.SetWidgetLayoutVisibility(true);
        UISceneManager.Instance.SetLayoutState((int)UIWidgetLayoutState.ShowAll);

        UISceneManager.Instance.PushPanel("ClaimRewardsPanel");
        UISceneManager.Instance.PushPanel("UpdatePanel");
    }
}
