using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    [Header("References")]
    public Canvas MainCanvas;

    private UIWidgetController widgetController;
    private UIPanelController panelController;

    public UIConfiguration UIConfiguration { get; private set; }    

    #region Singleton Pattern
    private static UISceneManager _instance;
    public static UISceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<UISceneManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(UISceneManager).ToString());
                    _instance = singletonObject.AddComponent<UISceneManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region Life cycle Methods
    void Awake()
    {
    }

    void Start()
    {
        // Startup logic can be added here in the future
    }

    void OnDestroy()
    {

    }
    #endregion

    #region Initialization Methods
    public void Initialize(UIConfiguration uiConfiguration)
    {
        UIConfiguration = uiConfiguration;

        widgetController = this.AddComponent<UIWidgetController>();
        panelController = this.AddComponent<UIPanelController>();

        widgetController.Initialize(uiConfiguration);
        panelController.Initialize(uiConfiguration, MainCanvas);
    }
    #endregion

    #region Panel Management Methods
    public void ShowPanel(int panelIndex, Dictionary<string, object> parameters = null)
    {
        UIPanel prefab = UIConfiguration.Panels[panelIndex];        
        panelController.ShowPanel(prefab, parameters);
    }
    #endregion
}
