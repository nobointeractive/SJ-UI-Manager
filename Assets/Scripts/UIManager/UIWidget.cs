using System;
using System.Collections.Generic;
using UnityEngine;

public enum UIWidgetVisibleState
{
    Unknown = -1,
    Hidden = 0,
    Visible = 1
}

public class UIWidget : MonoBehaviour
{
    [Header("References")]
    public List<Transform> AnimatableTargets;

    [Header("Settings")]
    [IntDropdown("WidgetAnimationTypes")]
    public int AnimationType;

    [VisibilityDropdown("WidgetLayoutStates")]
    public int VisibleState;

    public UIWidgetAnimator Animator { get; private set; }
    private UIWidgetVisibleState isVisible = UIWidgetVisibleState.Unknown;
    private UIWidgetVisibleState nextIsVisible = UIWidgetVisibleState.Unknown;
    private float animationTimeout = 0f;

    public void Initialize(UIWidgetAnimator animator)
    {
        Animator = animator;
    }

    public void Update()
    {
        if (animationTimeout > 0f)
        {
            animationTimeout -= Time.deltaTime;
        }
        else if (nextIsVisible != UIWidgetVisibleState.Unknown && isVisible != nextIsVisible)
        {
            isVisible = nextIsVisible;
            nextIsVisible = UIWidgetVisibleState.Unknown;

            if (isVisible == UIWidgetVisibleState.Visible)
            {
                animationTimeout = Animator.Show(this);
            }
            else
            {
                animationTimeout = Animator.Hide(this);
            }
        }
    }

    public void SetLayoutState(int state)
    {
        if ((VisibleState & state) != 0)
        {
            nextIsVisible = UIWidgetVisibleState.Visible;
        }
        else
        {
            nextIsVisible = UIWidgetVisibleState.Hidden;
        }
    }
}
