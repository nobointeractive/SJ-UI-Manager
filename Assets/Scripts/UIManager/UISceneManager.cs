using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum UICanvasLayer
{
    Widget = 0,
    Blackening = 1,
    Panel = 2,
    Overlay = 3,
    Count
}

public class UISceneManager : MonoBehaviour
{
    [Header("References")]
    public Canvas MainCanvas;

    private UIPanelController panelController;

    public UIConfiguration UIConfiguration { get; private set; }
    public UIWidgetLayout WidgetLayout { get; private set; }

    private List<Canvas> canvasLayers = new List<Canvas>();

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
    public void Initialize(UIConfiguration configuration, UIWidgetLayout widgetLayout)
    {
        UIConfiguration = configuration;

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

            if (i == (int)UICanvasLayer.Blackening)
            {
                GameObject blackeningObject = Instantiate(configuration.BlackeningPrefab, layerObject.transform);
                RectTransform blackeningRect = blackeningObject.GetComponent<RectTransform>();
                blackeningRect.anchorMin = Vector2.zero;
                blackeningRect.anchorMax = Vector2.one;
                blackeningRect.offsetMin = Vector2.zero;
                blackeningRect.offsetMax = Vector2.zero;
            }

            canvasLayers.Add(canvas);
        }

        var blackeningLayer = canvasLayers[(int)UICanvasLayer.Blackening].gameObject;
        blackeningLayer.SetActive(false);

        WidgetLayout = Instantiate(widgetLayout, canvasLayers[(int)UICanvasLayer.Widget].transform);
        WidgetLayout.Initialize(UIConfiguration);

        panelController = this.AddComponent<UIPanelController>();
        panelController.Initialize(configuration, canvasLayers[(int)UICanvasLayer.Panel], WidgetLayout, blackeningLayer);
    }
    #endregion

    #region Panel Management Methods
    public void ShowPanel(int panelIndex, Dictionary<string, object> parameters = null)
    {
        UIPanel prefab = UIConfiguration.Panels[panelIndex].prefab;
        panelController.ShowPanel(prefab, parameters);
    }

    public void ShowPanel(string name, Dictionary<string, object> parameters = null)
    {
        UIPanel prefab = UIConfiguration.Panels.Find(panel => panel.key == name).prefab;
        panelController.ShowPanel(prefab, parameters);
    }
    #endregion

    #region Widget Management Methods
    public void SetLayoutState(int state)
    {
        if (WidgetLayout != null)
        {
            WidgetLayout.SetLayoutState(state);
            Debug.Log($"Widget layout state set to: {state}");
        }
    }
    #endregion
}
