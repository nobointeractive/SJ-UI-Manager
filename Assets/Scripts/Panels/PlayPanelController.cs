using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPanelController : MonoBehaviour
{
    public void OnButtonClicked()
    {
        SceneManager.LoadSceneAsync("GamePlayScene");
    }
}
