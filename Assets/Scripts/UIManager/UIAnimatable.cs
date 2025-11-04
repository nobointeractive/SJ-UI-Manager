using UnityEngine;

public class UIAnimatable : MonoBehaviour
{
    [SerializeField] protected Transform[] animatableTargets;
    [SerializeField] private Transform holderTransform;
    [SerializeField] private Transform flyerTarget;

    [IntDropdown("AnimationTypes")]
    public int AppearanceAnimation;

    public UIAnimator AppearanceAnimator { get; private set; }

    [IntDropdown("AnimationTypes")]
    public int FlyerAnimation;
    public UIAnimator FlyerAnimator { get; private set; }

    public Transform[] AnimatableTargets => animatableTargets;
    public Transform HolderTransform => holderTransform;
    public Transform FlyerTarget => flyerTarget;

    public virtual void AttachAppearanceAnimator(UIAnimator animatorPrefab)
    {
        UIAnimator animator = Instantiate<UIAnimator>(animatorPrefab, transform);
        animator.Initialize(this);
        AppearanceAnimator = animator;
    }

    public virtual void AttachFlyerAnimator(UIAnimator animatorPrefab)
    {
        UIAnimator animator = Instantiate<UIAnimator>(animatorPrefab, transform);
        animator.Initialize(this);
        FlyerAnimator = animator;
    }
}
