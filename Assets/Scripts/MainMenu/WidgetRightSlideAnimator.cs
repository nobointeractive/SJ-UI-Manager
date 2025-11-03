using DG.Tweening;
using UnityEngine;

public class WidgetRightSlideAnimator : UIAnimator
{
    private float targetSizeX;

    public override void Initialize(UIAnimatable animatable)
    {
        base.Initialize(animatable);

        targetSizeX = 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            var targetSize = target.GetComponent<RectTransform>().sizeDelta;
            targetSizeX = Mathf.Max(targetSize.x, targetSizeX);
        }
    }

    public override float Show(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.gameObject.SetActive(true);
            target.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 1.5f + targetSizeX, 0);
            target.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f).SetEase(Ease.OutBack);
        }
        return 0.3f;
    }

    public override float Hide(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        animatable.gameObject.SetActive(true);
        foreach (var target in animatable.AnimatableTargets)
        {
            target.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 1.5f + targetSizeX, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                target.gameObject.SetActive(false);
            });
        }
        return 0.3f;
    }
}
