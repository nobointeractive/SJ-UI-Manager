using System;
using Aniloom.TAC.Controllers;
using Aniloom.TAC.Core;
using Aniloom.TAC.Orchestration;
using DG.Tweening;
using UnityEngine;

public class UITacAnimatedFlyerHolder : UIFlyerHolder
{
    [SerializeField] private TacPlayableBase showPlayable;
    [SerializeField] private TacPlayableBase hidePlayable;
    [SerializeField] private TacPlayableBase movePlayable;

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

    public override float AnimateMoveTo(UIWidget departure, UIWidget destination, float duration, Action onComplete)
    {
        if (movePlayable != null)
        {
            AssignMovePoints(movePlayable, departure, destination);
            movePlayable.Play();
            float totalDuration = movePlayable.Duration;
            DOVirtual.DelayedCall(totalDuration, () => onComplete?.Invoke());
            return totalDuration;
        }
        onComplete?.Invoke();
        return 0f;
    }
    
    private void AssignMovePoints(TacPlayableBase playable, UIWidget departure, UIWidget destination)
    {
        if (playable is TacOrchestrator orchestrator)
        {
            foreach (var child in orchestrator.children)
            {
                if (child.playable is TacPlayableBase childPlayable)
                {
                    AssignMovePoints(childPlayable, departure, destination);
                }
            }
        }
        else if (playable is TacMoveFromToController moveFromTo)
        {
            moveFromTo.fromPoints = new Transform[] { departure.transform };
            moveFromTo.toPoints = new Transform[] { destination.transform };
        }
    }
}
