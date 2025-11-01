using UnityEngine;

public class UIWidgetController : MonoBehaviour
{
    public UIConfiguration UIConfiguration { get; private set; }

    #region Initialization Methods
    public void Initialize(UIConfiguration uiConfiguration)
    {
        UIConfiguration = uiConfiguration;
    }
    #endregion
}
