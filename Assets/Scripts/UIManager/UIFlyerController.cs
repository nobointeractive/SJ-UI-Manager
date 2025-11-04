using System;
using System.Collections.Generic;
using UnityEngine;

public class UIFlyerController : MonoBehaviour
{
    public Canvas MainCanvas { get; private set; }
    public UIConfiguration UIConfiguration { get; private set; }

    public void Initialize(UIConfiguration configuration, Canvas overlayCanvas)
    {
        UIConfiguration = configuration;
        MainCanvas = overlayCanvas;
    }

    public void Play(UIAnimatable prefab, UIWidget start, UIWidget end, float duration)
    {
        UIAnimatable flyerInstance = Instantiate(prefab, MainCanvas.transform);
        flyerInstance.AttachAppearanceAnimator(UIConfiguration.Animators[flyerInstance.AppearanceAnimation]);
        flyerInstance.AttachFlyerAnimator(UIConfiguration.Animators[flyerInstance.FlyerAnimation]);

        if (start.FlyerAnimator != null)
        {
            start.FlyerAnimator.Show(start);
        }
        flyerInstance.gameObject.SetActive(true);
        flyerInstance.transform.position = start.FlyerTarget.transform.position;
        flyerInstance.FlyerAnimator.MoveTo(flyerInstance, end, duration);
    }
}
