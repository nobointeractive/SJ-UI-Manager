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
    [Header("Settings")]
    [IntDropdown("PanelHolderTypes")]
    public int WidgetHolderType;

    [VisibilityDropdown("WidgetLayoutStates")]
    public int VisibleState;

    [SerializeField] private Transform root;
    [SerializeField] private Transform[] flyerTargets;

    public bool IsAvailable = true; 
    
    public UIWidgetHolder WidgetHolder => widgetHolder;

    private UIWidgetVisibleState isVisible = UIWidgetVisibleState.Unknown;
    private UIWidgetVisibleState nextIsVisible = UIWidgetVisibleState.Unknown;
    private bool isKeepingVisible = false;
    private float animationTimeout = 0f;
    private UIWidgetHolder widgetHolder;

    public void Initialize(UIWidgetHolder holderPrefab)
    {
        if (widgetHolder == null)
        {
            widgetHolder = Instantiate(holderPrefab, transform);

            // Make widgetHolder rect transform center in this UIWidget rect transform
            RectTransform holderRect = widgetHolder.GetComponent<RectTransform>();
            holderRect.anchorMin = Vector2.zero;
            holderRect.anchorMax = Vector2.one;
            holderRect.offsetMin = Vector2.zero;
            holderRect.offsetMax = Vector2.zero;
        }

        widgetHolder.Initialize();
        root.SetParent(widgetHolder.HolderTransform, false);        

        isVisible = UIWidgetVisibleState.Unknown;
        nextIsVisible = UIWidgetVisibleState.Unknown;
        isKeepingVisible = false;
        animationTimeout = 0f;
        gameObject.SetActive(IsAvailable);
    }

    public float AnimationTimeout
    {
        get { return animationTimeout; }
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
                root.gameObject.SetActive(true);
                animationTimeout = WidgetHolder.AnimateShow();
            }
            return;
        }

        if (nextIsVisible != UIWidgetVisibleState.Unknown)
        {
            if (isVisible != nextIsVisible)
            {
                isVisible = nextIsVisible;
                nextIsVisible = UIWidgetVisibleState.Unknown;

                root.gameObject.SetActive(true);
                if (isVisible == UIWidgetVisibleState.Visible)
                {
                    animationTimeout = WidgetHolder.AnimateShow();
                }
                else
                {
                    animationTimeout = WidgetHolder.AnimateHide();
                }
            }
        }
        else
        {
            if (isVisible == UIWidgetVisibleState.Hidden)
            {
                root.gameObject.SetActive(false);
            }
        }
    }

    public void SetAvailability(bool available)
    {
        IsAvailable = available;
        gameObject.SetActive(available);
    }
    
    public void SetVisibility(bool visible)
    {
        if (visible)
        {
            nextIsVisible = UIWidgetVisibleState.Visible;
            transform.SetAsLastSibling();
        }
        else
        {
            nextIsVisible = UIWidgetVisibleState.Hidden;
            transform.SetAsFirstSibling();
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
        isKeepingVisible = true;
        transform.SetAsLastSibling();
    }

    public void StopKeepingVisible()
    {
        isKeepingVisible = false;
        transform.SetAsFirstSibling();
    }

    public Transform GetFlyerTarget(int index)
    {
        if (flyerTargets != null && index >= 0 && index < flyerTargets.Length)
        {
            return flyerTargets[index];
        }

        return transform;
    }
}
