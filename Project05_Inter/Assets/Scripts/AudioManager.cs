using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioChannel m_AudioChannel;
    [SerializeField]
    private AudioSource m_SFXAudioSource;

    private void Awake()
    {
        m_AudioChannel.OnSFXRequest += PlaySFX;
    }

    public void PlaySFX(AudioClip clip)
    {
        m_SFXAudioSource.clip = clip;
        m_SFXAudioSource.Play();
    }
}