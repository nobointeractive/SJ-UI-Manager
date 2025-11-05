using UnityEngine;

public class UIFlyer : UIAnimatable
{
    public virtual void Initialize()
    {
        gameObject.SetActive(true);
    }

    public virtual void Reset()
    {
        gameObject.SetActive(false);
    }
}
