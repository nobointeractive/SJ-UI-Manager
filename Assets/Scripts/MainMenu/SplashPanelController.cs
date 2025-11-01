using System.Collections;
using UnityEngine;

public class SplashPanelController : MonoBehaviour
{
    void Start()
    {
        // Initialize the splash panel
        StartCoroutine(LoadMainMenuAfterDelay(2f));
    }

    private IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SendMessage("OnClosePanel");
    }
}
