using UnityEngine;

public class UIAnimatable : MonoBehaviour
{
    [SerializeField] protected Transform[] animatableTargets;
    [SerializeField] private Transform holderTransform;
    [SerializeField] private Transform flyerTarget;

    [IntDropdown("AnimationTypes")]
    public int AppearanceAnimation;

    public UIAnimator Animator { get; set; }

    public Transform[] AnimatableTargets => animatableTargets;
    public Transform HolderTransform => holderTransform;
    public Transform FlyerTarget => flyerTarget;
}
