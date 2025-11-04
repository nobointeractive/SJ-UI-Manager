using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum UICanvasLayer
{
    Widget = 0,
    Panel = 1,
    Overlay = 2,
    Count
}

public class UISceneManager : MonoBehaviour
{
    [Header("References")]
    public Canvas MainCanvas;    

    public UIConfiguration UIConfiguration { get; private set; }

    private List<Canvas> canvasLayers = new List<Canvas>();

    private UIStatusController statusController;
    private UIPanelController panelController;
    private UIFlyerController flyerController;
    private UIWidgetController widgetController;

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
    public void Initialize(UIConfiguration configuration, UIWidgetController widgetLayout)
    {
        UIConfiguration = configuration;

        var blackeningObject = Instantiate(configuration.BlackeningPrefab.gameObject, MainCanvas.transform);
        UIAnimatable blackeningAnimatable = blackeningObject.GetComponent<UIAnimatable>();
        if (blackeningAnimatable != null)
        {
            blackeningAnimatable.AttachAppearanceAnimator(UIConfiguration.Animators[(int)blackeningAnimatable.AppearanceAnimation]);
        }
        blackeningAnimatable.gameObject.SetActive(false);

        for (int i = 0; i < (int)UICanvasLayer.Count; i++)
        {
            GameObject layerObject = new GameObject(((UICanvasLayer)i).ToString());
            layerObject.transform.SetParent(MainCanvas.transform, false);
            var canvas = layerObject.AddComponent<Canvas>();
            layerObject.AddComponent<GraphicRaycaster>();
            RectTransform rectTransform = layerObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            canvasLayers.Add(canvas);
        }

        var statusControllerObject = new GameObject("UIStatusController");
        statusControllerObject.transform.SetParent(transform, false);
        statusController = statusControllerObject.AddComponent<UIStatusController>();

        widgetController = Instantiate(widgetLayout, canvasLayers[(int)UICanvasLayer.Widget].transform);
        widgetController.Initialize(UIConfiguration, statusController);

        panelController = this.AddComponent<UIPanelController>();
        panelController.Initialize(configuration, canvasLayers[(int)UICanvasLayer.Panel], widgetController, blackeningAnimatable, statusController);

        flyerController = this.AddComponent<UIFlyerController>();
        flyerController.Initialize(configuration, canvasLayers[(int)UICanvasLayer.Overlay], statusController);
    }
    #endregion
    
    #region Status Query Methods
    public bool IsAnimating()
    {
        return statusController.IsAnimating();
    }
    #endregion

    #region Panel Management Methods
    public void ShowPanel(string name, Dictionary<string, object> parameters = null)
    {
        UIPanel prefab = UIConfiguration.GetPanel(name);
        panelController.ShowPanel(prefab, parameters);
    }

    public void PushPanel(string name, Dictionary<string, object> parameters = null)
    {
        UIPanel prefab = UIConfiguration.GetPanel(name);
        panelController.PushPanel(prefab, parameters);
    }
    #endregion

    #region Widget Management Methods
    public void SetLayoutState(int state)
    {
        if (widgetController != null)
        {
            widgetController.SetLayoutState(state);
            Debug.Log($"Widget layout state set to: {state}");
        }
    }

    public void SetWidgetLayoutVisibility(bool visible)
    {
        if (widgetController != null)
        {
            widgetController.gameObject.SetActive(visible);
        }
    }

    public UIWidget GetWidget(string name)
    {
        if (widgetController != null)
        {
            return widgetController.GetWidget(name);
        }
        return null;
    }
    #endregion

    #region Flyer Management Methods
    public void PlayFlyer(string flyerName, UIWidget start, UIWidget end, float duration)
    {
        UIAnimatable flyerPrefab = UIConfiguration.GetFlyer(flyerName);
        if (flyerPrefab != null)
        {
            flyerController.Play(flyerPrefab, start, end, duration);
        }
    }
    #endregion
}
