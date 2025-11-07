using UnityEngine;

public class UIWidgetController : MonoBehaviour
{
    public UIConfiguration UIConfiguration { get; private set; }
    public UIStatusController StatusController { get; private set; }
    [SerializeField] protected UIWidget[] widgets;

    private int currentState = -1;
    private int nextState = -1;   

    public void Initialize(UIConfiguration configuration, UIStatusController statusController)
    {
        UIConfiguration = configuration;
        StatusController = statusController;
        currentState = -1;

        foreach (UIWidget widget in widgets)
        {
            widget.Initialize(UIConfiguration.WidgetHolders[widget.WidgetHolderType]);
        }
    }

    public void Update()
    {
        if (nextState != -1 && nextState != currentState)
        {
            currentState = nextState;
            nextState = -1;

            foreach (UIWidget widget in widgets)
            {
                widget.SetLayoutState(currentState);
            }
        }

        foreach (UIWidget widget in widgets)
        {
            StatusController.TrackAnimationEndingTime(widget.AnimationTimeout);
        }
    }

    public void SetLayoutState(int state)
    {
        nextState = state;
    }

    public void SetToDefaultLayoutState()
    {
        SetLayoutState(UIConfiguration.DefaultWidgetLayoutState);
    }
    
    public UIWidget GetWidget(string name)
    {
        return System.Array.Find(widgets, widget => widget.name.Equals(name));
    }
}
