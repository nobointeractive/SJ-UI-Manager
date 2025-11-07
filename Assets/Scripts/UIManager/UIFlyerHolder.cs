using UnityEngine;

public class UIFlyerHolder : MonoBehaviour
{
    [SerializeField] private Transform holderTransform;
    public Transform HolderTransform => holderTransform;

    public virtual void Initialize()
    {
    }

    public virtual float AnimateShow()
    {
        gameObject.SetActive(true);
        return 0f;
    }

    public virtual float AnimateHide()
    {
        gameObject.SetActive(false);
        return 0f;
    }

    public virtual float AnimateMoveTo(UIWidget departure, UIWidget destination, float duration, System.Action onComplete)
    {
        onComplete?.Invoke();
        return 0f;
    }
}
