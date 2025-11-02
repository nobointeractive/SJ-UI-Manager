using DG.Tweening;
using UnityEngine;

public class UIWidgetAnimator : MonoBehaviour
{
    public virtual float Show(UIWidget widget)
    {
        if (widget == null) return 0;
        foreach (var target in widget.AnimatableTargets)
        {
            target.gameObject.SetActive(true);
        }
        return 0;
    }

    public virtual float Hide(UIWidget widget)
    {
        if (widget == null) return 0;
        foreach (var target in widget.AnimatableTargets)
        {
            target.gameObject.SetActive(false);
        }
        return 0;
    }
}
