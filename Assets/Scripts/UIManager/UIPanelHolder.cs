using UnityEngine;

public class UIPanelHolder : MonoBehaviour
{
    [SerializeField] private Transform holderTransform;
    public Transform HolderTransform => holderTransform;

    protected UIPanel panel;

    public virtual void Initialize(UIPanel panel)
    {
        gameObject.SetActive(true);
        this.panel = panel;
    }

    public void ClosePanel()
    {
        panel?.ClosePanel();
    }

    public virtual float AnimateShow()
    {
        gameObject.SetActive(true);
        return 0f;
    }

    public virtual float AnimateHide()
    {
        gameObject.SetActive(false);
        return 0f;
    }
}
