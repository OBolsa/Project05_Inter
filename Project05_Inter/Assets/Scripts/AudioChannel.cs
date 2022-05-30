using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Audio/Audio Channel")]
public class AudioChannel : ScriptableObject
{
    public delegate void AudioCallback(AudioClip clip);
    public AudioCallback OnSFXRequest;

    public void RequestAudio(AudioClip clip)
    {
        OnSFXRequest?.Invoke(clip);
    }
}
