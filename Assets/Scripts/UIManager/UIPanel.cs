using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [IntDropdown("PanelHolderTypes")]
    public int PanelHolderType;

    [IntDropdown("PanelAnimatorTypes")]
    public int PanelAnimatorType;

    private UIPanelHolder panelHolder;
    private UIPanelController panelController;

    public UIPanelHolder PanelHolder => panelHolder;

    #region Initialization Methods
    public void Initialize(UIPanelController controller, UIPanelHolder holder, Dictionary<string, object> parameters)
    {
        panelController = controller;
        panelHolder = holder;
        if (parameters == null)
        {
            parameters = new Dictionary<string, object>();
        }
        SendMessage("OnPanelInitialized", parameters, SendMessageOptions.DontRequireReceiver);
    }
    #endregion

    #region Unity Event Handlers
    private void OnClosePanel()
    {
        panelController.ClosePanel(this);
    }
    #endregion
}
