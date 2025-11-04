using System;
using System.Collections.Generic;
using UnityEngine;

public class UIFlyerController : MonoBehaviour
{
    public Canvas MainCanvas { get; private set; }
    public UIConfiguration UIConfiguration { get; private set; }
    public UIStatusController StatusController { get; private set; }

    public void Initialize(UIConfiguration configuration, Canvas overlayCanvas, UIStatusController statusController)
    {
        UIConfiguration = configuration;
        MainCanvas = overlayCanvas;
        StatusController = statusController;
    }

    public void Play(UIAnimatable prefab, UIWidget start, UIWidget end, float duration)
    {
        UIAnimatable flyerInstance = Instantiate(prefab, MainCanvas.transform);
        flyerInstance.AttachAppearanceAnimator(UIConfiguration.Animators[flyerInstance.AppearanceAnimation]);
        flyerInstance.AttachFlyerAnimator(UIConfiguration.Animators[flyerInstance.FlyerAnimation]);

        if (start.FlyerAnimator != null)
        {
            start.AnimateShow();
        }
        flyerInstance.gameObject.SetActive(true);
        float timeout = flyerInstance.AnimateMoveTo(start, end, duration);
        StatusController.TrackAnimationEndingTime(timeout);
    }
}
