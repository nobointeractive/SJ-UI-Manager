using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIKeyValuePair
{
    public string key;
    public string value;
}

public enum UIPanelVisibilityState
{
    Shown,
    Hidden,
    Closed
}

public class UIPanel : MonoBehaviour
{
    [IntDropdown("PanelHolderTypes")]
    public int PanelHolderType;

    [IntDropdown("AnimationTypes")]
    public int AnimationType;

    [IntDropdown("WidgetLayoutStates")]
    public int WidgetLayoutState;

    [SerializeField] private List<UIKeyValuePair> staticParameters = new List<UIKeyValuePair>();
    private Dictionary<string, string> _staticParametersDict;
    public Dictionary<string, string> StaticParameters
    {
        get
        {
            if (_staticParametersDict != null)
            {
                return _staticParametersDict;
            }
            _staticParametersDict = new Dictionary<string, string>();
            foreach (var pair in staticParameters)
            {
                _staticParametersDict[pair.key] = pair.value;
            }
            return _staticParametersDict;
        }
    }

    [HideInInspector] public UIPanelVisibilityState VisibilityState { get; set; }

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

    #region Public Methods
    public void ClosePanel()
    {
        panelController.ClosePanel(this);
    }
    #endregion

    #region Unity Event Handlers
    public void OnClosePanel()
    {
        ClosePanel();
    }
    #endregion
}
