using UnityEngine;

public class UIPanelHolder : MonoBehaviour
{
    [SerializeField] private Transform holderTransform;
    [SerializeField] private Transform[] animatableTargets;

    public Transform HolderTransform => holderTransform;
    public Transform[] AnimatableTargets => animatableTargets;

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
