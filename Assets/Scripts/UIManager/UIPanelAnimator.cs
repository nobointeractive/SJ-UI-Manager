using DG.Tweening;
using UnityEngine;

public class UIPanelAnimator : MonoBehaviour
{
    public virtual float Show(UIPanel panel)
    {
        if (panel == null) return 0;
        panel.gameObject.SetActive(true);
        return 0;
    }

    public virtual float Hide(UIPanel panel)
    {
        if (panel == null) return 0;
        panel.gameObject.SetActive(false);
        return 0;
    }
}
