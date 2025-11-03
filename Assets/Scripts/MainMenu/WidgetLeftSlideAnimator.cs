using DG.Tweening;
using UnityEngine;

public class WidgetLeftSlideAnimator : UIAnimator
{
    public override float Show(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.gameObject.SetActive(true);
            target.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width / 2, 0);
            target.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f).SetEase(Ease.OutBack);
        }
        return 0.3f;
    }

    public override float Hide(UIAnimatable animatable)
    {
        if (animatable == null) return 0;
        foreach (var target in animatable.AnimatableTargets)
        {
            target.GetComponent<RectTransform>().DOAnchorPosX(-Screen.width / 2, 0.3f).SetEase(Ease.InBack);
        }
        return 0.3f;
    }
}
