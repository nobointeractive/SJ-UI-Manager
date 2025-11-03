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

    [IntDropdown("WidgetLayoutStates")]
    public int DefaultWidgetLayoutState;
    [SerializeField] private GameObject blackeningPrefab;

    public List<UIKeyValuePanelPair> Panels => panels;
    public List<UIPanelHolder> PanelHolders => panelHolders;
    public List<UIAnimator> Animators => animators;
    public GameObject BlackeningPrefab => blackeningPrefab;
}
