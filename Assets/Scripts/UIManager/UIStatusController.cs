using UnityEngine;

public class UIStatusController : MonoBehaviour
{
    private float animationEndingTime = 0f;

    public bool IsAnimating()
    {
        return Time.timeSinceLevelLoad < animationEndingTime;
    }

    public void TrackAnimationEndingTime(float time)
    {
        if (time <= 0f) return;
        float endingTime = Time.timeSinceLevelLoad + time;
        if (endingTime > animationEndingTime)
        {
            animationEndingTime = endingTime;
        }
    }
}
