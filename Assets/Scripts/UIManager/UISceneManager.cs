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
    public UIWidgetLayout WidgetLayout { get; private set; }

    private List<Canvas> canvasLayers = new List<Canvas>();
    private UIPanelController panelController;

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

        GameObject blackeningObject = Instantiate(configuration.BlackeningPrefab, MainCanvas.transform);
        UIAnimatable blackeningAnimatable = blackeningObject.GetComponent<UIAnimatable>();
        if (blackeningAnimatable != null)
        {
            var animator = UIConfiguration.Animators[(int)blackeningAnimatable.AppearanceAnimation];
            animator.Initialize(blackeningAnimatable);
        }
        blackeningObject.SetActive(false);

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

        WidgetLayout = Instantiate(widgetLayout, canvasLayers[(int)UICanvasLayer.Widget].transform);
        WidgetLayout.Initialize(UIConfiguration);

        panelController = this.AddComponent<UIPanelController>();
        panelController.Initialize(configuration, canvasLayers[(int)UICanvasLayer.Panel], WidgetLayout, blackeningAnimatable);
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
