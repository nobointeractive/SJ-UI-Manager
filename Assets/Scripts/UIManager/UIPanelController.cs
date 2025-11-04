using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    public class UIPanelCommand
    {
        public enum CommandType
        {
            ShowNew,
            CloseOld,
            PushNew
        }

        public CommandType Type;
        public UIPanel Panel;
        public Dictionary<string, object> Parameters;

        public UIPanelCommand(CommandType type, UIPanel panel = null, Dictionary<string, object> parameters = null)
        {
            Type = type;
            Panel = panel;
            Parameters = parameters;
        }
    }

    public Canvas MainCanvas { get; private set; }
    public UIConfiguration UIConfiguration { get; private set; }
    public UIWidgetController WidgetLayout { get; private set; }
    public UIAnimatable BlackeningLayer { get; private set; }

    private List<UIPanel> panelStack = new List<UIPanel>();
    private List<UIPanelCommand> commandQueue = new List<UIPanelCommand>();
    private List<UIPanel> panelsToRemove = new List<UIPanel>();
    private Dictionary<int, Stack<UIPanelHolder>> holderPool = new Dictionary<int, Stack<UIPanelHolder>>();
    private Dictionary<string, Stack<UIPanel>> panelPool = new Dictionary<string, Stack<UIPanel>>();
    private float animationTimeout = 0f;
    private bool isRunningTransition = false;
    private bool isBlackeningShown = false;

    #region Life cycle Methods
    void Update()
    {
        if (!isRunningTransition)
        {
            if (animationTimeout > 0f)
            {
                animationTimeout -= Time.deltaTime;
            }
            else
            {
                ProcessPanelsToRemove();
                ProcessCommandQueue();

                if (commandQueue.Count == 0 && panelStack.Count == 0 && isBlackeningShown)
                {
                    HideBlackening();
                    WidgetLayout.SetToDefaultLayoutState();                        
                }
            }
        }    
    }
    #endregion

    #region Initialization Methods
    public void Initialize(UIConfiguration uiConfiguration, Canvas mainCanvas, UIWidgetController widgetLayout, UIAnimatable blackeningAnimatable)
    {
        UIConfiguration = uiConfiguration;
        MainCanvas = mainCanvas;
        WidgetLayout = widgetLayout;
        BlackeningLayer = blackeningAnimatable;
    }
    #endregion

    #region Panel Management Methods
    public void ShowPanel(UIPanel prefab, Dictionary<string, object> parameters = null)
    {
        var command = new UIPanelCommand(UIPanelCommand.CommandType.ShowNew, prefab, parameters);        
        commandQueue.Add(command);
    }

    public void ClosePanel(UIPanel panel)
    {
        var command = new UIPanelCommand(UIPanelCommand.CommandType.CloseOld, panel);
        commandQueue.Add(command);
    }
    
    public void PushPanel(UIPanel prefab, Dictionary<string, object> parameters = null)
    {
        var command = new UIPanelCommand(UIPanelCommand.CommandType.PushNew, prefab, parameters);
        commandQueue.Add(command);
    }

    private IEnumerator DoShowNewPanel(UIPanel prefab, Dictionary<string, object> parameters)
    {
        if (panelStack.Count > 0)
        {
            var prevPanel = panelStack[panelStack.Count - 1];
            if (prevPanel.VisibilityState == UIPanelVisibilityState.Shown)
            {
                prevPanel.VisibilityState = UIPanelVisibilityState.Hidden;
                animationTimeout = prevPanel.PanelHolder.AppearanceAnimator.Hide(prevPanel.PanelHolder);
                if (animationTimeout > 0f)
                {
                    yield return new WaitForSeconds(animationTimeout);
                }
            }
        }

        UIPanelHolder holder = CreateNewPanelHolderInstance((int)prefab.PanelHolderType, MainCanvas.transform);
        UIPanel panel = CreateNewPanelInstance(prefab, holder.HolderTransform);        
        panel.Initialize(this, holder, parameters);
        holder.Initialize(UIConfiguration.Animators[(int)prefab.AppearanceAnimation], panel);
        panelStack.Add(panel);
        panel.transform.SetAsLastSibling();
        panel.VisibilityState = UIPanelVisibilityState.Shown;
        animationTimeout = holder.AppearanceAnimator.Show(panel.PanelHolder);
        WidgetLayout.SetLayoutState(panel.WidgetLayoutState);

        if (!isBlackeningShown)
        {
            ShowBlackening();
        }

        isRunningTransition = false;
    }

    private IEnumerator DoCloseOldPanel(UIPanel panel)
    {
        if (!panelStack.Contains(panel))
        {
            isRunningTransition = false;
            yield break;
        }

        panelStack.Remove(panel);

        UIPanel topPanel = null;
        if (panelStack.Count > 0)
        {
            topPanel = panelStack[panelStack.Count - 1];
        }

        if (panel.VisibilityState == UIPanelVisibilityState.Shown)
        {
            panel.gameObject.SendMessage("OnPanelClosed", SendMessageOptions.DontRequireReceiver);

            panel.VisibilityState = UIPanelVisibilityState.Hidden;
            animationTimeout = panel.PanelHolder.AppearanceAnimator.Hide(panel.PanelHolder);
            yield return new WaitForSeconds(animationTimeout * UIConfiguration.PanelDelayTimeScaleBetweenAnimations);
            panelsToRemove.Add(panel);
        }
        else if (panel.VisibilityState == UIPanelVisibilityState.Hidden)
        {
            panelsToRemove.Add(panel);
        }

        if (topPanel != null && topPanel.VisibilityState == UIPanelVisibilityState.Hidden)
        {
            topPanel.transform.SetAsLastSibling();
            topPanel.VisibilityState = UIPanelVisibilityState.Shown;
            topPanel.PanelHolder.gameObject.SetActive(true);
            animationTimeout = topPanel.PanelHolder.AppearanceAnimator.Show(topPanel.PanelHolder);
            WidgetLayout.SetLayoutState(topPanel.WidgetLayoutState);
        }

        isRunningTransition = false;
    }
    
    private IEnumerator DoPushNewPanel(UIPanel prefab, Dictionary<string, object> parameters)
    {
        if (panelStack.Count > 0)
        {
            UIPanelHolder holder = CreateNewPanelHolderInstance((int)prefab.PanelHolderType, MainCanvas.transform);
            UIPanel panel = CreateNewPanelInstance(prefab, holder.HolderTransform);
            panel.Initialize(this, holder, parameters);
            holder.Initialize(UIConfiguration.Animators[(int)prefab.AppearanceAnimation], panel);
            panelStack.Insert(0, panel);

            panel.VisibilityState = UIPanelVisibilityState.Hidden;
            holder.gameObject.SetActive(false);

            isRunningTransition = false;
        }
        else
        {
            yield return DoShowNewPanel(prefab, parameters);
        }
    }

    private void ProcessCommandQueue()
    {
        if (commandQueue.Count == 0) return;

        var command = commandQueue[0];
        commandQueue.RemoveAt(0);

        isRunningTransition = true;
        switch (command.Type)
        {
            case UIPanelCommand.CommandType.ShowNew:
                StartCoroutine(DoShowNewPanel(command.Panel, command.Parameters));
                break;
            case UIPanelCommand.CommandType.CloseOld:
                StartCoroutine(DoCloseOldPanel(command.Panel));
                break;
            case UIPanelCommand.CommandType.PushNew:
                StartCoroutine(DoPushNewPanel(command.Panel, command.Parameters));
                break;
        }
    }
    
    private void ProcessPanelsToRemove()
    {
        if (panelsToRemove.Count == 0) return;

        foreach (var panel in panelsToRemove)
        {
            if (panel == null || panel.VisibilityState == UIPanelVisibilityState.Closed)
            {
                continue;
            }

            var holderIndex = panel.PanelHolderType;
            if (!holderPool.ContainsKey(holderIndex))
            {
                holderPool[holderIndex] = new Stack<UIPanelHolder>();
            }
            panel.PanelHolder.transform.SetParent(this.transform, false);
            panel.PanelHolder.gameObject.SetActive(false);
            holderPool[holderIndex].Push(panel.PanelHolder);

            var panelName = panel.name;
            if (!panelPool.ContainsKey(panelName))
            {
                panelPool[panelName] = new Stack<UIPanel>();
            }
            panel.transform.SetParent(this.transform, false);

            panel.gameObject.SetActive(false);
            panel.VisibilityState = UIPanelVisibilityState.Closed;
            panelPool[panelName].Push(panel);            
        }

        panelsToRemove.Clear();
    }

    private UIPanelHolder CreateNewPanelHolderInstance(int panelHolderIndex, Transform parent)
    {
        var prefab = UIConfiguration.PanelHolders[panelHolderIndex];
        if (!holderPool.ContainsKey(panelHolderIndex))
        {
            holderPool[panelHolderIndex] = new Stack<UIPanelHolder>();
        }

        var pool = holderPool[panelHolderIndex];
        UIPanelHolder holder;
        if (pool.Count > 0)
        {
            holder = pool.Pop();
            holder.transform.SetParent(parent, false);
        }
        else
        {
            holder = Instantiate<UIPanelHolder>(prefab, parent);
        }

        holder.gameObject.SetActive(true);
        return holder;
    }

    private UIPanel CreateNewPanelInstance(UIPanel prefab, Transform parent)
    {
        if (!panelPool.ContainsKey(prefab.name))
        {
            panelPool[prefab.name] = new Stack<UIPanel>();
        }

        var pool = panelPool[prefab.name];
        UIPanel panel;
        if (pool.Count > 0)
        {
            panel = pool.Pop();
            panel.transform.SetParent(parent, false);
        }
        else
        {
            panel = Instantiate(prefab, parent);
            panel.name = prefab.name;
            panel.gameObject.SetActive(true);
        }

        return panel;
    }
    #endregion
    

    #region Blackening Layer Methods
    public void ShowBlackening()
    {
        isBlackeningShown = true;
        BlackeningLayer.AppearanceAnimator.Show(BlackeningLayer);
    }

    public void HideBlackening()
    {
        isBlackeningShown = false;
        BlackeningLayer.AppearanceAnimator.Hide(BlackeningLayer);
    }
    #endregion
}

