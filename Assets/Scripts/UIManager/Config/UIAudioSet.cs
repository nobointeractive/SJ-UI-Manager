using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIAudioSet", menuName = "UI/UI Audio Set", order = 2)]
public class UIAudioSet : ScriptableObject
{
    public AudioClip Open;
    public AudioClip Close;
    public AudioClip Launch;
    public AudioClip Land;
    public AudioClip PositiveClick;
    public AudioClip NegativeClick;
}
