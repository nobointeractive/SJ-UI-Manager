using System;
using Aniloom.TAC.Core;
using Aniloom.TAC.Orchestration;
using DG.Tweening;
using UnityEngine;

public class UITacAnimatedPanelHolder : UIPanelHolder
{
    [SerializeField] private TacPlayableBase showOrchestrator;
    [SerializeField] private TacPlayableBase hideOrchestrator;

    public override float AnimateShow()
    {
        if (showOrchestrator != null)
        {
            showOrchestrator.Play();
            return showOrchestrator.Duration;
        }
        return 0f;
    }

    public override float AnimateHide()
    {
        if (hideOrchestrator != null)
        {
            hideOrchestrator.Play();
            return hideOrchestrator.Duration;
        }
        return 0f;        
    }
}
