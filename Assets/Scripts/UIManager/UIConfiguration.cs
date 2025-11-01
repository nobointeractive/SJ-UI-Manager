using System.Collections.Generic;
using UnityEngine;

public class UIConfiguration : ScriptableObject
{
    [SerializeField] private List<UIPanel> panels;
    [SerializeField] private List<UIPanelHolder> panelHolders;
    [SerializeField] private List<UIPanelAnimator> panelAnimators;

    public List<UIPanel> Panels => panels;
    public List<UIPanelHolder> PanelHolders => panelHolders;
    public List<UIPanelAnimator> PanelAnimators => panelAnimators;
}
