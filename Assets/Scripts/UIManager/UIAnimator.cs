using DG.Tweening;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    public virtual void Initialize(UIAnimatable animatable)
    {
        animatable.Animator = this;
    }

    public virtual float Show(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.gameObject.SetActive(true);
        }
        return 0;
    }

    public virtual float Hide(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.gameObject.SetActive(false);
        }
        return 0;
    }

    public virtual float FlyTo(UIAnimatable animatable, UIAnimatable destination)
    {
        if (animatable == null || destination == null) return 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.position = destination.FlyerTarget.position;
        }
        return 0;
    }
}
