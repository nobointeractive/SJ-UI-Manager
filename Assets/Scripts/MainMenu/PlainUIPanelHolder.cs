using UnityEngine;
using TMPro;

public class PlainUIPanelHolder : UITacAnimatedPanelHolder
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI titleText;

    public override void Initialize( UIPanel panel)
    {
        base.Initialize(panel);

        // Set the title if provided in static parameters of the panel data
        var staticParameters = panel.StaticParameters;
        if (staticParameters != null && staticParameters.ContainsKey("Title"))
        {
            SetTitle(staticParameters["Title"]);
        }
    }

    public void SetTitle(string title)
    {
        if (titleText != null)
        {
            titleText.text = title;
        }
    }
}
