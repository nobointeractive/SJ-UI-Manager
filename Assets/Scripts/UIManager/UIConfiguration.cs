using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIKeyValuePanelPair
{
    public string key;
    public UIPanel prefab;
}

[CreateAssetMenu(fileName = "UIConfiguration", menuName = "UI/UI Configuration", order = 1)]
public class UIConfiguration : ScriptableObject
{
    [SerializeField] private List<UIKeyValuePanelPair> panels;
    [SerializeField] private List<UIPanelHolder> panelHolders;
    [SerializeField] private List<UIAnimator> animators;
    [SerializeField] private List<UIAnimatable> flyers;
    [SerializeField] private GameObject blackeningPrefab;

    [IntDropdown("WidgetLayoutStates")]
    public int DefaultWidgetLayoutState;    

    public float PanelDelayTimeScaleBetweenAnimations = 0.5f;

    public List<UIKeyValuePanelPair> Panels => panels;
    public List<UIPanelHolder> PanelHolders => panelHolders;
    public List<UIAnimator> Animators => animators;
    public List<UIAnimatable> Flyers => flyers;
    public GameObject BlackeningPrefab => blackeningPrefab;

    public UIAnimatable GetFlyer(string name)
    {
        return flyers.Find(flyer => flyer.name.Equals(name));
    }
}
