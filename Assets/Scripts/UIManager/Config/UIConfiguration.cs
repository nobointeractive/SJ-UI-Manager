using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIConfiguration", menuName = "UI/UI Configuration", order = 1)]
public class UIConfiguration : ScriptableObject
{
    [SerializeField] private List<UIPanel> panels;
    [SerializeField] private List<UIPanelHolder> panelHolders;
    [SerializeField] private List<UIWidgetHolder> widgetHolders;
    [SerializeField] private List<UIAnimator> animators;
    [SerializeField] private List<UIFlyer> flyers;
    [SerializeField] private List<UIAudioSet> audioSets;
    [SerializeField] private UIAnimatable blackeningPrefab;

    [IntDropdown("WidgetLayoutStates")]
    public int DefaultWidgetLayoutState;    

    public float PanelDelayTimeScaleBetweenAnimations = 0.5f;

    public List<UIPanel> Panels => panels;
    public List<UIPanelHolder> PanelHolders => panelHolders;
    public List<UIWidgetHolder> WidgetHolders => widgetHolders;
    public List<UIAnimator> Animators => animators;
    public List<UIFlyer> Flyers => flyers;
    public UIAnimatable BlackeningPrefab => blackeningPrefab;

    public UIPanel GetPanel(string name)
    {
        return panels.Find(panel => panel.name.Equals(name));
    }

    public UIFlyer GetFlyer(string name)
    {
        return flyers.Find(flyer => flyer.name.Equals(name));
    }

    public UIAnimator GetAnimator(int index)
    {
        if (index >= 0 && index < animators.Count)
        {
            return animators[index];
        }
        return null;
    }

    public UIAnimator GetAnimator(int index1, int index2)
    {
        UIAnimator selected = null;
        if (index1 >= 0 && index1 < animators.Count)
        {
            selected = animators[index1];
        }
        if (index1 == 0 || selected == null)
        {
            if (index2 > 0 && index2 < animators.Count)
            {
                selected = animators[index2];
            }
        }
        return selected;
    }
}
