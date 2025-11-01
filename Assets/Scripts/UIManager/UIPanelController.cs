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

    private List<UIPanel> panelStack = new List<UIPanel>();
    private List<UIPanelCommand> commandQueue = new List<UIPanelCommand>();
    private Dictionary<int, Stack<UIPanelHolder>> holderPool = new Dictionary<int, Stack<UIPanelHolder>>();
    private Dictionary<string, Stack<UIPanel>> panelPool = new Dictionary<string, Stack<UIPanel>>();
    private float animationTimeout = 0f;

    #region Life cycle Methods
    void Update()
    {
        if (animationTimeout > 0f)
        {
            animationTimeout -= Time.deltaTime;
        }
        else
        {
            ProcessCommandQueue();
        }    
    }
    #endregion

    #region Initialization Methods
    public void Initialize(UIConfiguration uiConfiguration, Canvas mainCanvas)
    {
        UIConfiguration = uiConfiguration;
        MainCanvas = mainCanvas;
    }
    #endregion

    #region Panel Management Methods
    public void ShowPanel(UIPanel prefab, Dictionary<string, object> parameters = null)
    {
        var command = new UIPanelCommand(UIPanelCommand.CommandType.ShowNew, prefab, parameters);
        if (panelStack.Count == 0)
        {
            commandQueue.Add(command);
        }
        else
        {
            var hidePreviousCommand = new UIPanelCommand(UIPanelCommand.CommandType.HideOld, panelStack[panelStack.Count - 1]);
            commandQueue.Add(hidePreviousCommand);
            commandQueue.Add(command);
        }
    }

    public void ClosePanel(UIPanel panel)
    {
        var command = new UIPanelCommand(UIPanelCommand.CommandType.CloseOld, panel);
        if (panelStack.Count == 0)
        {
            commandQueue.Add(command);
        }
        else
        {
            commandQueue.Add(command);
            var showPreviousCommand = new UIPanelCommand(UIPanelCommand.CommandType.ShowOld, panelStack[panelStack.Count - 1]);            
            commandQueue.Add(showPreviousCommand);
        }
    }

    private void DoShowNewPanel(UIPanel prefab, Dictionary<string, object> parameters)
    {
        UIPanelHolder holder = CreateNewPanelHolderInstance((int)prefab.PanelHolderType, MainCanvas.transform);
        UIPanel panel = CreateNewPanelInstance(prefab, holder.HolderTransform);
        UIPanelAnimator animator = UIConfiguration.PanelAnimators[(int)prefab.PanelAnimatorType];
        panel.Initialize(this, holder, parameters);
        panelStack.Add(panel);
        animationTimeout = animator.Show(panel);
    }

    private void DoShowOldPanel(UIPanel panel)
    {
        if (!panelStack.Contains(panel)) return;
        UIPanelAnimator animator = UIConfiguration.PanelAnimators[(int)panel.PanelAnimatorType];     
        animationTimeout = animator.Show(panel);
    }

    private void DoHideOldPanel(UIPanel panel)
    {
        if (!panelStack.Contains(panel)) return;
        UIPanelAnimator animator = UIConfiguration.PanelAnimators[(int)panel.PanelAnimatorType];
        animationTimeout = animator.Hide(panel);
    }

    private void DoCloseOldPanel(UIPanel panel)
    {
        if (!panelStack.Contains(panel)) return;
        UIPanelAnimator animator = UIConfiguration.PanelAnimators[(int)panel.PanelAnimatorType];
        panelStack.Remove(panel);
        animationTimeout = animator.Hide(panel);
    }
    
    private void ProcessCommandQueue()
    {
        if (commandQueue.Count == 0) return;

        var command = commandQueue[0];
        commandQueue.RemoveAt(0);

        switch (command.Type)
        {
            case UIPanelCommand.CommandType.ShowNew:
                DoShowNewPanel(command.Panel, command.Parameters);
                break;
            case UIPanelCommand.CommandType.HideOld:
                DoHideOldPanel(command.Panel);
                break;
            case UIPanelCommand.CommandType.CloseOld:
                DoCloseOldPanel(command.Panel);
                break;
            case UIPanelCommand.CommandType.ShowOld:
                DoShowOldPanel(command.Panel);
                break;
        }
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
        }
        else
        {
            panel = Instantiate(prefab, parent);
        }

        panel.gameObject.SetActive(true);
        panel.transform.SetParent(parent, false);
        return panel;
    }
    #endregion
}

