using UnityEngine;

[CreateAssetMenu(fileName = "GameUIConfiguration", menuName = "UI/GameUIConfiguration")]
public class GameUIConfiguration : UIConfiguration
{
    private int splashPanelIndex = -1;

    public int SplashPanelIndex
    {
        get
        {
            if (splashPanelIndex == -1)
            {
                splashPanelIndex = Panels.FindIndex(panel => panel.name == "SplashPanel");
            }
            return splashPanelIndex;
        }
    }
}
