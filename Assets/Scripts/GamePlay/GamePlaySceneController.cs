using System.Collections.Generic;
using UnityEngine;

public class GamePlaySceneController : MonoBehaviour
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
        UISceneManager.Instance.SetWidgetLayoutVisibility(true);
        UISceneManager.Instance.SetLayoutState((int)UIWidgetLayoutState.ShowAll);
    }
}
