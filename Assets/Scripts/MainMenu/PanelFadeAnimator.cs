using DG.Tweening;
using UnityEngine;
using System.Linq;

public class PanelFadeAnimator : UIPanelAnimator
{
    public override float Show(UIPanel panel)
    {
        if (panel == null) return 0;
        panel.gameObject.SetActive(true);
        foreach (var target in panel.PanelHolder.AnimatableTargets)
        {
            var cg = target.GetComponent<CanvasGroup>();
            cg.DOFade(1, 0.3f).From(0);
        }
        return 0.3f;
    }

    public override float Hide(UIPanel panel)
    {
        if (panel == null) return 0;
        panel.gameObject.SetActive(true);
        foreach (var target in panel.PanelHolder.AnimatableTargets)
        {
            var cg = target.GetComponent<CanvasGroup>();
            cg.DOFade(0, 0.3f).From(1).OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
            });
        }
        return 0.3f;
    }
}
