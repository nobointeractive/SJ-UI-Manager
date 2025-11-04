using UnityEngine;

public class PlayButtonController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        UISceneManager.Instance.ShowPanel("PlayPanel");
    }
}
