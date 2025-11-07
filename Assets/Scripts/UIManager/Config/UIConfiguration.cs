using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfiguration", menuName = "UI/UI Configuration", order = 1)]
public class UIConfiguration : ScriptableObject
{
    [SerializeField] private List<UIPanel> panels;
    [SerializeField] private List<UIFlyer> flyers;
    [SerializeField] private List<UIPanelHolder> panelHolders;
    [SerializeField] private List<UIWidgetHolder> widgetHolders;
    [SerializeField] private List<UIFlyerHolder> flyerHolders;
    [SerializeField] private List<UIAudioSet> audioSets;
    [SerializeField] private string[] widgetLayoutStates;
    [SerializeField] private UIWidget blackeningPrefab;    

    [IntDropdown("WidgetLayoutStates")]
    public int DefaultWidgetLayoutState;    

    public float PanelDelayTimeScaleBetweenAnimations = 0.5f;

    public List<UIPanel> Panels => panels;
    public List<UIFlyer> Flyers => flyers;
    public List<UIPanelHolder> PanelHolders => panelHolders;
    public List<UIWidgetHolder> WidgetHolders => widgetHolders;
    public List<UIFlyerHolder> FlyerHolders => flyerHolders;
    public UIWidget BlackeningPrefab => blackeningPrefab;
    public string[] WidgetLayoutStates => widgetLayoutStates;
    public List<UIAudioSet> AudioSets => audioSets;

    public UIPanel GetPanel(string name)
    {
        return panels.Find(panel => panel.name.Equals(name));
    }

    public UIFlyer GetFlyer(string name)
    {
        return flyers.Find(flyer => flyer.name.Equals(name));
    }
}
