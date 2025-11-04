using UnityEngine;

public class UIAnimatable : MonoBehaviour
{    
    [SerializeField] protected Transform[] animatableTargets;        

    [IntDropdown("AnimationTypes")]
    public int AppearanceAnimation;

    public UIAnimator AppearanceAnimator { get; private set; }

    [IntDropdown("AnimationTypes")]
    public int FlyerAnimation;
    public UIAnimator FlyerAnimator { get; private set; }

    public Transform[] AnimatableTargets => animatableTargets;            

    public virtual void AttachAppearanceAnimator(UIAnimator animatorPrefab)
    {
        if (AppearanceAnimator != null)
        {
            if (AppearanceAnimator.OriginalPrefab == animatorPrefab.gameObject)
            {
                return;
            }
            Destroy(AppearanceAnimator.gameObject);
        }
        UIAnimator animator = Instantiate<UIAnimator>(animatorPrefab, transform);
        animator.OriginalPrefab = animatorPrefab.gameObject;
        animator.Initialize(this);
        AppearanceAnimator = animator;
    }

    public virtual void AttachFlyerAnimator(UIAnimator animatorPrefab)
    {
        if (FlyerAnimator != null)
        {
            if (FlyerAnimator.OriginalPrefab == animatorPrefab.gameObject)
            {
                return;
            }
            Destroy(FlyerAnimator.gameObject);
        }
        UIAnimator animator = Instantiate<UIAnimator>(animatorPrefab, transform);
        animator.OriginalPrefab = animatorPrefab.gameObject;
        animator.Initialize(this);
        FlyerAnimator = animator;
    }
}
