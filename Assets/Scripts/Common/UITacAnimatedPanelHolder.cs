using System;
using Aniloom.TAC.Core;
using Aniloom.TAC.Orchestration;
using DG.Tweening;
using UnityEngine;

public class UITacAnimatedPanelHolder : UIPanelHolder
{
    [SerializeField] private TacPlayableBase showPlayable;
    [SerializeField] private TacPlayableBase hidePlayable;

    public override void Initialize(UIPanel panel)
    {
        base.Initialize(panel);

        var cg = HolderTransform.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 1f;
    }

    public override float AnimateShow()
    {
        if (showPlayable != null)
        {
            showPlayable.Play();
            return showPlayable.Duration;
        }
        return 0f;
    }

    public override float AnimateHide()
    {
        if (hidePlayable != null)
        {
            hidePlayable.Play();
            return hidePlayable.Duration;
        }
        return 0f;        
    }
}
