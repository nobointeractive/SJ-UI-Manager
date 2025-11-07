using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlyerController : MonoBehaviour
{
    public Canvas MainCanvas { get; private set; }
    public UIConfiguration UIConfiguration { get; private set; }
    public UIStatusController StatusController { get; private set; }

    private Dictionary<string, Stack<UIFlyer>> flyersPool = new Dictionary<string, Stack<UIFlyer>>();

    public void Initialize(UIConfiguration configuration, Canvas overlayCanvas, UIStatusController statusController)
    {
        UIConfiguration = configuration;
        MainCanvas = overlayCanvas;
        StatusController = statusController;
    }

    public void Play(UIFlyer prefab, UIWidget departure, UIWidget destination, float duration)
    {
        UIFlyer flyerInstance = createFlyerInstance(prefab);
        flyerInstance.Initialize();

        destination.KeepVisible();
        departure.WidgetHolder.AnimateLaunchFlyer();        
        float timeout = flyerInstance.AnimateMoveTo(departure, destination, duration, (hideDuration) =>
        {
            StartCoroutine(LandFlyer(flyerInstance, destination, hideDuration));
        });
        StatusController.TrackAnimationEndingTime(timeout);
    }

    private IEnumerator LandFlyer(UIFlyer flyerInstance, UIWidget destination, float hideDuration)
    {
        float landDuration = destination.WidgetHolder.AnimateLandFlyer();
        yield return new WaitForSeconds(Mathf.Max(landDuration, hideDuration));
        destination.StopKeepingVisible();

        flyerInstance.Reset();
        recycleFlyerInstance(flyerInstance);
    }

    private UIFlyer createFlyerInstance(UIFlyer prefab)
    {
        UIFlyer flyerInstance;

        if (!flyersPool.ContainsKey(prefab.name))
        {
            flyersPool[prefab.name] = new Stack<UIFlyer>();
        }

        if (flyersPool[prefab.name].Count > 0)
        {
            flyerInstance = flyersPool[prefab.name].Pop();
        }
        else
        {
            flyerInstance = Instantiate(prefab, MainCanvas.transform);
        }

        flyerInstance.AttachAppearanceAnimator(UIConfiguration.GetAnimator(flyerInstance.AppearanceAnimation));
        flyerInstance.AttachFlyerAnimator(UIConfiguration.GetAnimator(flyerInstance.FlyerAnimation));
        flyerInstance.gameObject.SetActive(true);
        return flyerInstance;
    }
    
    private void recycleFlyerInstance(UIFlyer flyerInstance)
    {
        if (!flyersPool.ContainsKey(flyerInstance.name))
        {
            flyersPool[flyerInstance.name] = new Stack<UIFlyer>();
        }
        flyerInstance.gameObject.SetActive(false);
        flyersPool[flyerInstance.name].Push(flyerInstance);
    }
}
