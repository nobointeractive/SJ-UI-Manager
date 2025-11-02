using DG.Tweening;
using UnityEngine;

public class PanelScaleAnimator : UIPanelAnimator
{
    public override float Show(UIPanel panel)
    {
        if (panel == null) return 0;
        panel.gameObject.SetActive(true);
        foreach (var target in panel.PanelHolder.AnimatableTargets)
        {
            target.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).From(Vector3.zero);
        }
        return 0.3f;
    }

    public override float Hide(UIPanel panel)
    {
        if (panel == null) return 0;
        panel.gameObject.SetActive(true);
        foreach (var target in panel.PanelHolder.AnimatableTargets)
        {
            target.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
            });
        }
        return 0.3f;
    }
}
