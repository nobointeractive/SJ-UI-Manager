using DG.Tweening;
using UnityEngine;

public class FlyerMoveAnimator : UIAnimator
{
    public override void Initialize(UIAnimatable animatable)
    {
        base.Initialize(animatable);
    }

    public override float MoveTo(UIAnimatable animatable, UIAnimatable destination)
    {
        if (animatable == null || destination == null) return 0;
        Vector2 destinationPosition = destination.FlyerTarget.GetComponent<RectTransform>().anchoredPosition;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.GetComponent<RectTransform>().DOAnchorPos(destinationPosition, 0.3f).SetEase(Ease.InOutSine);
        }
        return 0;
    }
}
