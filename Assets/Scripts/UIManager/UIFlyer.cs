using UnityEngine;

public class UIFlyer : MonoBehaviour
{
    [IntDropdown("PanelHolderTypes")]
    public int FlyerHolderType;

    [SerializeField] private Transform root;

    public UIFlyerHolder FlyerHolder => flyerHolder;

    private UIFlyerHolder flyerHolder;

    public virtual void Initialize(UIFlyerHolder holderPrefab)
    {
        if (flyerHolder == null)
        {
            flyerHolder = Instantiate(holderPrefab, transform);
        }

        flyerHolder.Initialize();
        root.SetParent(flyerHolder.HolderTransform, false);
                
        gameObject.SetActive(true);
    }

    public virtual void Reset()
    {
        gameObject.SetActive(false);
    }
}
