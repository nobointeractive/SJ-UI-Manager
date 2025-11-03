using UnityEngine;

public class UIPanelHolder : UIAnimatable
{

    protected UIPanel panel;

    public virtual void Initialize(UIPanel panel)
    {
        this.panel = panel;
    }

    public void ClosePanel()
    {
        panel?.ClosePanel();
    }
}
