using UnityEngine;

public class UIWidgetLayout : MonoBehaviour
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
            UIAnimator animator = UIConfiguration.Animators[widget.AnimationType];
            widget.Initialize(animator);
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
}
