using System;
using DG.Tweening;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    public GameObject OriginalPrefab { get; set; }

    public virtual void Initialize(UIAnimatable animatable)
    {
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

    public virtual float LaunchFlyer(UIAnimatable animatable)
    {
        return 0;
    }
    
    public virtual float LandFlyer(UIAnimatable animatable)
    {        
        return 0;
    }

    public virtual float MoveTo(UIAnimatable animatable, UIWidget departure, UIWidget destination, float duration, Action<float> onCompleted)
    {
        if (animatable == null || destination == null) return 0;

        foreach (var target in animatable.AnimatableTargets)
        {
            target.position = destination.FlyerTarget.position;
        }
        
        onCompleted?.Invoke(0f);
        return duration;
    }
}
