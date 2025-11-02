using UnityEngine;
using TMPro;

public class PlainUIPanelHolder : UIPanelHolder
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI titleText;

    public override void Initialize(UIPanel panel)
    {
        base.Initialize(panel);

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
