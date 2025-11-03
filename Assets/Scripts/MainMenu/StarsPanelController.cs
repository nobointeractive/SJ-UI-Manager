using UnityEngine;

public class StarsPanelController : MonoBehaviour
{
    public void OnButtonClicked()
    {
        GetComponent<UIPanel>().ClosePanel();
        UISceneManager.Instance.ShowPanel("Play");
    }
}
