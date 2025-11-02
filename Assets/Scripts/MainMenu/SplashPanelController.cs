using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashPanelController : MonoBehaviour
{
    private UIPanel panel;

    private Action OnComplete;

    void Awake()
    {
        panel = GetComponent<UIPanel>();
    }

    #region Unity Event Handlers
    public void OnPanelInitialized(Dictionary<string, object> parameters)
    {
        Debug.Log("Splash Panel Initialized");

        if (parameters != null && parameters.ContainsKey("OnComplete"))
        {
            OnComplete = parameters["OnComplete"] as Action;
        }

        StartCoroutine(DoneAfterDelay(2f));
    }

    public void OnPanelClosed()
    {
        Debug.Log("Splash Panel Closed");

        OnComplete?.Invoke();
    }
    #endregion

    private IEnumerator DoneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        panel.ClosePanel();
    }
}
