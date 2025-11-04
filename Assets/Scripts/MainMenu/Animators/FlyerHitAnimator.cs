using DG.Tweening;
using UnityEngine;

public class FlyerHitAnimator : UIAnimator
{
    public override void Initialize(UIAnimatable animatable)
    {
        base.Initialize(animatable);
    }

    public override float Show(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).From(Vector3.one * 1.1f);
        }
        return 0.3f;
    }

    public override float Hide(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBack).From(Vector3.one * 0.9f);
        }
        return 0.3f;
    }
}
