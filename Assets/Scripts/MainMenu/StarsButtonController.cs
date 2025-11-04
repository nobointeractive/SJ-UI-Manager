using UnityEngine;

public class StarsButtonController : MonoBehaviour
{
    public void OnButtonClicked()
    {
        UISceneManager.Instance.ShowPanel("StarsPanel");
    }
}
