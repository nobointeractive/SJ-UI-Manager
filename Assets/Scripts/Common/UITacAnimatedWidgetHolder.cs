using System;
using Aniloom.TAC.Core;
using Aniloom.TAC.Orchestration;
using DG.Tweening;
using UnityEngine;

public class UITacAnimatedWidgetHolder : UIWidgetHolder
{
    [SerializeField] private TacPlayableBase showPlayable;
    [SerializeField] private TacPlayableBase hidePlayable;

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
