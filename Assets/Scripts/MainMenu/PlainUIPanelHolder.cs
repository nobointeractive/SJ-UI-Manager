using UnityEngine;
using TMPro;

public class PlainUIPanelHolder : UIPanelHolder
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI titleText;

    public override void Initialize(UIAnimator animator, UIPanel panel)
    {
        base.Initialize(animator, panel);

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
