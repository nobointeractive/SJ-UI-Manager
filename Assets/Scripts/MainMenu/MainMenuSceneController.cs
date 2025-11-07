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
    public AudioSource audioSource;

    void Awake()
    {
        // Initialize the UISceneManager with configuration and widget layout prefab
        UISceneManager.Instance.Initialize(uiConfiguration, widgetLayout, audioSource);

        // Hide the widget layout at the start
        UISceneManager.Instance.SetLayoutState((int)UIWidgetLayoutState.HideAll);
        UISceneManager.Instance.SetWidgetLayoutVisibility(false);
    }

    private void Start()
    {
        // Show the splash panel. This will fake some loading and call ShowMainMenu when done.
        UISceneManager.Instance.ShowPanel("SplashPanel", new Dictionary<string, object>
        {
            { "OnComplete", new System.Action(ShowMainMenu) }
        });
    }

    public void ShowMainMenu()
    {
        // Show the widget layout and set the state to show all widgets
        UISceneManager.Instance.SetWidgetLayoutVisibility(true);
        UISceneManager.Instance.SetLayoutState((int)UIWidgetLayoutState.ShowAll);

        // Show some panels that are automatically displayed in some cases.
        // PushPanel means they are pushed to the stack and wait for current panel to close before showing.
        UISceneManager.Instance.PushPanel("ClaimRewardsPanel");
        UISceneManager.Instance.PushPanel("UpdatePanel");
    }
}
