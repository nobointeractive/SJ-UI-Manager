using DG.Tweening;
using UnityEngine;

public class PanelScaleAnimator : UIAnimator
{
    public override float Show(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        animatable.gameObject.SetActive(true);
        foreach (var target in animatable.AnimatableTargets)
        {
            target.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).From(Vector3.zero);
        }
        return 0.3f;
    }

    public override float Hide(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        animatable.gameObject.SetActive(true);
        foreach (var target in animatable.AnimatableTargets)
        {
            target.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                animatable.gameObject.SetActive(false);
            });
        }
        return 0.3f;
    }
}
