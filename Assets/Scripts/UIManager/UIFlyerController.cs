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
        var animator = UIConfiguration.Animators[(int)flyerInstance.AppearanceAnimation];
        animator.Initialize(flyerInstance);

        if (start.FlyerAnimator != null)
        {
            start.FlyerAnimator.Show(start);
        }
        flyerInstance.gameObject.SetActive(true);
        flyerInstance.transform.position = start.FlyerTarget.transform.position;
        flyerInstance.AppearanceAnimator.MoveTo(flyerInstance, end, duration);
    }
}
