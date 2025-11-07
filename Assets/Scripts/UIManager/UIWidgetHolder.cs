using UnityEngine;

public class UIWidgetHolder : MonoBehaviour
{
    [SerializeField] private Transform holderTransform;
    public Transform HolderTransform => holderTransform;

    public virtual void Initialize()
    {
        gameObject.SetActive(true);
    }

    public virtual float AnimateShow()
    {
        return 0f;
    }

    public virtual float AnimateHide()
    {
        return 0f;
    }

    public virtual float AnimateLaunchFlyer()
    {
        return 0f;
    }

    public virtual float AnimateLandFlyer()
    {
        return 0f;
    }
}
