using System;
using System.Collections.Generic;
using UnityEngine;

public enum UIWidgetVisibleState
{
    Unknown = -1,
    Hidden = 0,
    Visible = 1
}

public class UIWidget : UIAnimatable
{
    [Header("Settings")]
    [VisibilityDropdown("WidgetLayoutStates")]
    public int VisibleState;

    private UIWidgetVisibleState isVisible = UIWidgetVisibleState.Unknown;
    private UIWidgetVisibleState nextIsVisible = UIWidgetVisibleState.Unknown;
    private UIWidgetVisibleState stayVisibleState = UIWidgetVisibleState.Unknown;
    private float animationTimeout = 0f;

    public void Initialize(UIAnimator appearanceAnimator, UIAnimator flyerAnimator)
    {
        AttachAppearanceAnimator(appearanceAnimator);
        AttachFlyerAnimator(flyerAnimator);
    }

    public void Update()
    {
        if (animationTimeout > 0f)
        {
            animationTimeout -= Time.deltaTime;
            return;
        }

        if (stayVisibleState != UIWidgetVisibleState.Unknown)
        {
            if (isVisible != stayVisibleState)
            {
                isVisible = stayVisibleState;
                if (isVisible == UIWidgetVisibleState.Visible)
                {
                    animationTimeout = AppearanceAnimator.Show(this);
                }
                else
                {
                    animationTimeout = AppearanceAnimator.Hide(this);
                }
            }
            return;
        }

        if (nextIsVisible != UIWidgetVisibleState.Unknown)
        {
            if (isVisible != nextIsVisible)
            {
                isVisible = nextIsVisible;
                nextIsVisible = UIWidgetVisibleState.Unknown;

                if (isVisible == UIWidgetVisibleState.Visible)
                {
                    animationTimeout = AppearanceAnimator.Show(this);
                }
                else
                {
                    animationTimeout = AppearanceAnimator.Hide(this);
                }
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

    public void KeepVisible()
    {
        nextIsVisible = isVisible;
        stayVisibleState = UIWidgetVisibleState.Visible;
    }

    public void StopKeepingVisible()
    {
        stayVisibleState = UIWidgetVisibleState.Unknown;
    }
}
