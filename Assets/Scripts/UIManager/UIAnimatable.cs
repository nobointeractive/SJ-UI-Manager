using UnityEngine;

public class UIAnimatable : MonoBehaviour
{
    [SerializeField] protected Transform[] animatableTargets;
    [SerializeField] private Transform holderTransform;

    [IntDropdown("AnimationTypes")]
    public int AnimationType;

    public Transform[] AnimatableTargets => animatableTargets;    
    public Transform HolderTransform => holderTransform;
}
