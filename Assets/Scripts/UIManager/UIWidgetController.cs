using UnityEngine;

public class UIWidgetController : MonoBehaviour
{
    public UIConfiguration UIConfiguration { get; private set; }
    [SerializeField] protected UIWidget[] widgets;

    private int currentState = -1;
    private int nextState = -1;
    private float animationTimeout = 0f;
    private int defaultState = -1;

    public void Initialize(UIConfiguration configuration)
    {
        UIConfiguration = configuration;
        defaultState = configuration.DefaultWidgetLayoutState;
        currentState = -1;

        foreach (UIWidget widget in widgets)
        {
            UIAnimator appearanceAnimator = UIConfiguration.Animators[widget.AppearanceAnimation];            
            UIAnimator flyerAnimator = UIConfiguration.Animators[widget.FlyerAnimation];
            widget.Initialize(appearanceAnimator, flyerAnimator);
        }
    }

    public void Update()
    {
        if (animationTimeout > 0f)
        {
            animationTimeout -= Time.deltaTime;
        }
        else
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
        }
    }

    public void SetLayoutState(int state)
    {
        nextState = state;
    }

    public void SetToDefaultLayoutState()
    {
        SetLayoutState(defaultState);
    }
    
    public UIWidget GetWidget(string name)
    {
        return System.Array.Find(widgets, widget => widget.name.Equals(name));
    }
}
