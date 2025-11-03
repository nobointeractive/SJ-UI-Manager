using DG.Tweening;
using UnityEngine;
using System.Linq;

public class PanelFadeAnimator : UIAnimator
{
    public override float Show(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        animatable.gameObject.SetActive(true);
        foreach (var target in animatable.AnimatableTargets)
        {
            var cg = target.GetComponent<CanvasGroup>();
            cg.DOFade(1, 0.3f).From(0);
        }
        return 0.3f;
    }

    public override float Hide(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        animatable.gameObject.SetActive(true);
        foreach (var target in animatable.AnimatableTargets)
        {
            var cg = target.GetComponent<CanvasGroup>();
            cg.DOFade(0, 0.3f).From(1);
        }
        return 0.3f;
    }
}
