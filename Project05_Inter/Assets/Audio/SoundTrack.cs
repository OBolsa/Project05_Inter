using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{
    [SerializeField]
    private BGMHandler m_BGMHandler;
    [SerializeField]
    private BGAHandler m_BGAHandler;

    private void Start()
    {
        m_BGMHandler.DoAudio();
        m_BGAHandler.DoAudio();
    }
}
