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

    [SerializeField] private Transform flyerTarget;   

    public bool IsAvailable = true; 

    private UIWidgetVisibleState isVisible = UIWidgetVisibleState.Unknown;
    private UIWidgetVisibleState nextIsVisible = UIWidgetVisibleState.Unknown;
    private bool isKeepingVisible = false;
    private float animationTimeout = 0f;

    public void Initialize(UIAnimator appearanceAnimator, UIAnimator flyerAnimator)
    {
        AttachAppearanceAnimator(appearanceAnimator);
        AttachFlyerAnimator(flyerAnimator);
        gameObject.SetActive(IsAvailable);
    }

    public void Update()
    {
        if (animationTimeout > 0f)
        {
            animationTimeout -= Time.deltaTime;
            return;
        }

        if (isKeepingVisible)
        {
            if (isVisible != UIWidgetVisibleState.Visible)
            {
                isVisible = UIWidgetVisibleState.Visible;
                animationTimeout = AnimateShow();
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
                    animationTimeout = AnimateShow();
                }
                else
                {
                    animationTimeout = AnimateHide();
                }
            }
        }
    }
    
    public void SetAvailability(bool available)
    {
        IsAvailable = available;
        gameObject.SetActive(available);
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
        isKeepingVisible = true;
    }

    public void StopKeepingVisible()
    {
        isKeepingVisible = false;
    }

    public Transform FlyerTarget
    {
        get
        {
            if (flyerTarget != null)
                return flyerTarget;
            
            return this.transform;
        }
    }
}
