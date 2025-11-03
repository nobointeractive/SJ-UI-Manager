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
            ShowOld,
            HideOld,
            CloseOld
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
    public UIWidgetLayout WidgetLayout { get; private set; }
    public GameObject BlackeningLayer { get; private set; }

    private List<UIPanel> panelStack = new List<UIPanel>();
    private List<UIPanelCommand> commandQueue = new List<UIPanelCommand>();
    private List<UIPanel> panelsToRemove = new List<UIPanel>();
    private Dictionary<int, Stack<UIPanelHolder>> holderPool = new Dictionary<int, Stack<UIPanelHolder>>();
    private Dictionary<string, Stack<UIPanel>> panelPool = new Dictionary<string, Stack<UIPanel>>();
    private float animationTimeout = 0f;
    private bool isRunningTransition = false;

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

                if (commandQueue.Count == 0 && panelStack.Count == 0)
                {
                    WidgetLayout.SetToDefaultLayoutState();
                }
            }
        }    
    }
    #endregion

    #region Initialization Methods
    public void Initialize(UIConfiguration uiConfiguration, Canvas mainCanvas, UIWidgetLayout widgetLayout, GameObject blackeningLayer)
    {
        UIConfiguration = uiConfiguration;
        MainCanvas = mainCanvas;
        WidgetLayout = widgetLayout;
        BlackeningLayer = blackeningLayer;
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

    private IEnumerator DoShowNewPanel(UIPanel prefab, Dictionary<string, object> parameters)
    {
        UIAnimator animator;

        if (panelStack.Count > 0)
        {
            var prevPanel = panelStack[panelStack.Count - 1];
            if (prevPanel.VisibilityState == UIPanelVisibilityState.Shown)
            {
                prevPanel.VisibilityState = UIPanelVisibilityState.Hidden;
                animator = UIConfiguration.Animators[(int)prevPanel.AnimationType];
                animationTimeout = animator.Hide(prevPanel.PanelHolder);
                if (animationTimeout > 0f)
                {
                    yield return new WaitForSeconds(animationTimeout);
                }
            }
        }

        UIPanelHolder holder = CreateNewPanelHolderInstance((int)prefab.PanelHolderType, MainCanvas.transform);
        UIPanel panel = CreateNewPanelInstance(prefab, holder.HolderTransform);
        animator = UIConfiguration.Animators[(int)prefab.AnimationType];
        panel.Initialize(this, holder, parameters);
        holder.Initialize(panel);
        panelStack.Add(panel);
        panel.transform.SetAsLastSibling();
        panel.VisibilityState = UIPanelVisibilityState.Shown;
        animationTimeout = animator.Show(panel.PanelHolder);
        WidgetLayout.SetLayoutState(panel.WidgetLayoutState);

        isRunningTransition = false;
    }

    private void DoShowOldPanel(UIPanel panel)
    {
        if ((!panelStack.Contains(panel)) || panel.VisibilityState == UIPanelVisibilityState.Shown)
        {
            isRunningTransition = false;
            return;
        }
        panel.transform.SetAsLastSibling();
        panel.VisibilityState = UIPanelVisibilityState.Shown;
        UIAnimator animator = UIConfiguration.Animators[(int)panel.AnimationType];
        animationTimeout = animator.Show(panel.PanelHolder);
        WidgetLayout.SetLayoutState(panel.WidgetLayoutState);
        isRunningTransition = false;
    }

    private void DoHideOldPanel(UIPanel panel)
    {
        if ((!panelStack.Contains(panel)) || panel.VisibilityState == UIPanelVisibilityState.Hidden)
        {
            isRunningTransition = false;
            return;
        }
        panel.VisibilityState = UIPanelVisibilityState.Hidden;
        UIAnimator animator = UIConfiguration.Animators[(int)panel.AnimationType];
        animationTimeout = animator.Hide(panel.PanelHolder);
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
            UIAnimator animator = UIConfiguration.Animators[(int)panel.AnimationType];
            animationTimeout = animator.Hide(panel.PanelHolder);
            yield return new WaitForSeconds(animationTimeout);
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
            UIAnimator animator = UIConfiguration.Animators[(int)topPanel.AnimationType];
            animationTimeout = animator.Show(topPanel.PanelHolder);
            WidgetLayout.SetLayoutState(topPanel.WidgetLayoutState);
        }

        isRunningTransition = false;
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
            case UIPanelCommand.CommandType.HideOld:
                DoHideOldPanel(command.Panel);
                break;
            case UIPanelCommand.CommandType.CloseOld:
                StartCoroutine(DoCloseOldPanel(command.Panel));
                break;
            case UIPanelCommand.CommandType.ShowOld:
                DoShowOldPanel(command.Panel);
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
            holder.transform.localPosition = Vector3.zero;
            holder.transform.localRotation = Quaternion.identity;
            holder.transform.localScale = Vector3.one;
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
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localRotation = Quaternion.identity;
            panel.transform.localScale = Vector3.one;
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
}

