using UnityEngine;

public class UIPanelHolder : UIAnimatable
{

    protected UIPanel panel;

    public virtual void Initialize(UIAnimator animator, UIPanel panel)
    {
        gameObject.SetActive(true);

        this.panel = panel;
        animator.Initialize(this);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        foreach (var target in AnimatableTargets)
        {
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.identity;
            target.localScale = Vector3.one;

            var cg = target.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 1;
            }
        }
    }

    public void ClosePanel()
    {
        panel?.ClosePanel();
    }
}
