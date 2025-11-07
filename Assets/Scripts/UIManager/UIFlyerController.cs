using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlyerController : MonoBehaviour
{
    public Canvas MainCanvas { get; private set; }
    public UIConfiguration UIConfiguration { get; private set; }
    public UIStatusController StatusController { get; private set; }
    public AudioSource AudioPlayer { get; private set; }

    private Dictionary<string, Stack<UIFlyer>> flyersPool = new Dictionary<string, Stack<UIFlyer>>();    

    public void Initialize(UIConfiguration configuration, Canvas overlayCanvas, UIStatusController statusController, AudioSource audioSource = null)
    {
        UIConfiguration = configuration;
        MainCanvas = overlayCanvas;
        StatusController = statusController;
        AudioPlayer = audioSource;
    }

    public void Play(UIFlyer prefab, UIWidget departure, UIWidget destination, float duration)
    {
        UIFlyer flyerInstance = createFlyerInstance(prefab);
        flyerInstance.Initialize(UIConfiguration.FlyerHolders[prefab.FlyerHolderType]);

        destination.KeepVisible();
        if (departure.WidgetHolder != null)
        {
            departure.WidgetHolder.AnimateLaunchFlyer();
            AudioPlayer?.PlayOneShot(UIConfiguration.AudioSets[departure.AudioSetType].Launch);
        }

        flyerInstance.FlyerHolder.AnimateShow();
        float timeout = flyerInstance.FlyerHolder.AnimateMoveTo(departure, destination, duration, () =>
        {
            StartCoroutine(LandFlyer(flyerInstance, destination));
        });
        StatusController.TrackAnimationEndingTime(timeout);
    }

    private IEnumerator LandFlyer(UIFlyer flyerInstance, UIWidget destination)
    {
        float hideDuration = flyerInstance.FlyerHolder.AnimateHide();
        float landDuration = destination.WidgetHolder != null ? destination.WidgetHolder.AnimateLandFlyer() : 0f;
        AudioPlayer?.PlayOneShot(UIConfiguration.AudioSets[destination.AudioSetType].Land);
        yield return new WaitForSeconds(Mathf.Max(landDuration, hideDuration));
        destination.StopKeepingVisible();

        flyerInstance.CleanUp();
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

        flyerInstance.gameObject.SetActive(true);
        flyerInstance.name = prefab.name;
        return flyerInstance;
    }

    private void recycleFlyerInstance(UIFlyer flyerInstance)
    {
        string prefabName = flyerInstance.name.Replace("(Clone)", "").Trim();
        if (!flyersPool.ContainsKey(prefabName))
        {
            flyersPool[prefabName] = new Stack<UIFlyer>();
        }
        flyerInstance.transform.SetParent(transform, false);
        flyerInstance.gameObject.SetActive(false);
        flyersPool[prefabName].Push(flyerInstance);
    }
}
