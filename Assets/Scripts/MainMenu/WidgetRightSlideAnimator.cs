using DG.Tweening;
using UnityEngine;

public class WidgetRightSlideAnimator : UIWidgetAnimator
{
    public override float Show(UIWidget widget)
    {
        if (widget == null) return 0;
        foreach (var target in widget.AnimatableTargets)
        {
            target.gameObject.SetActive(true);
            target.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 1.5f, 0);
            target.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f).SetEase(Ease.OutBack);
        }
        return 0.3f;
    }

    public override float Hide(UIWidget widget)
    {
        if (widget == null) return 0;
        widget.gameObject.SetActive(true);
        foreach (var target in widget.AnimatableTargets)
        {
            target.GetComponent<RectTransform>().DOAnchorPosX(Screen.width * 1.5f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                target.gameObject.SetActive(false);
            });
        }
        return 0.3f;
    }
}
