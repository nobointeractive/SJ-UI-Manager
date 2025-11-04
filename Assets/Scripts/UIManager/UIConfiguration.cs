using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfiguration", menuName = "UI/UI Configuration", order = 1)]
public class UIConfiguration : ScriptableObject
{
    [SerializeField] private List<UIPanel> panels;
    [SerializeField] private List<UIPanelHolder> panelHolders;
    [SerializeField] private List<UIAnimator> animators;
    [SerializeField] private List<UIAnimatable> flyers;
    [SerializeField] private UIAnimatable blackeningPrefab;

    [IntDropdown("WidgetLayoutStates")]
    public int DefaultWidgetLayoutState;    

    public float PanelDelayTimeScaleBetweenAnimations = 0.5f;

    public List<UIPanel> Panels => panels;
    public List<UIPanelHolder> PanelHolders => panelHolders;
    public List<UIAnimator> Animators => animators;
    public List<UIAnimatable> Flyers => flyers;
    public UIAnimatable BlackeningPrefab => blackeningPrefab;

    public UIPanel GetPanel(string name)
    {
        return panels.Find(panel => panel.name.Equals(name));
    }

    public UIAnimatable GetFlyer(string name)
    {
        return flyers.Find(flyer => flyer.name.Equals(name));
    }
}
