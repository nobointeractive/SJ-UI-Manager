using UnityEngine;

public class UIAnimatable : MonoBehaviour
{
    [SerializeField] protected Transform[] animatableTargets;
    [SerializeField] private Transform holderTransform;
    [SerializeField] private Transform flyerTarget;

    [IntDropdown("AnimationTypes")]
    public int AppearanceAnimation;

    public UIAnimator AppearanceAnimator { get; set; }

    [IntDropdown("AnimationTypes")]
    public int FlyerAnimation;
    public UIAnimator FlyerAnimator { get; set; }

    public Transform[] AnimatableTargets => animatableTargets;
    public Transform HolderTransform => holderTransform;
    public Transform FlyerTarget => flyerTarget;
}
