using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlyerMoveAnimator : UIAnimator
{
    public override void Initialize(UIAnimatable animatable)
    {
        base.Initialize(animatable);
    }

    public override float MoveTo(UIAnimatable animatable, UIWidget destination, float duration = 1f)
    {
        if (animatable == null || destination == null) return 0;
        animatable.AppearanceAnimator.Show(animatable);
        StartCoroutine(DOMoveTo(animatable, destination, duration));
        return duration;
    }

    private IEnumerator DOMoveTo(UIAnimatable animatable, UIWidget destination, float duration = 1f)
    {
        destination.KeepVisible();

        List<Vector3> startPositions = new List<Vector3>();
        for (int i = 0; i < animatable.AnimatableTargets.Length; i++)
        {
            startPositions.Add(animatable.AnimatableTargets[i].transform.position);
        }
            
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            for (int i = 0; i < animatable.AnimatableTargets.Length; i++)
            {
                var target = animatable.AnimatableTargets[i];
                target.transform.position = Vector3.Lerp(startPositions[i], destination.FlyerTarget.position, t);
            }
            yield return null;
        }

        animatable.AppearanceAnimator.Hide(animatable);
        float hideDuration = destination.FlyerAnimator.Hide(destination);

        yield return new WaitForSeconds(hideDuration);
        destination.StopKeepingVisible();
    }
}
