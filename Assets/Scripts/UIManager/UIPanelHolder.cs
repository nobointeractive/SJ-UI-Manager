using UnityEngine;

public class UIPanelHolder : MonoBehaviour
{
    [SerializeField] private Transform holderTransform;
    [SerializeField] private Transform[] animatableTargets;

    public Transform HolderTransform => holderTransform;
    public Transform[] AnimatableTargets => animatableTargets;
}
