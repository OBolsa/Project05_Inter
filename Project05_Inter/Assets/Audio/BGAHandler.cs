using UnityEngine;

[System.Serializable]
public class BGAHandler
{
    [SerializeField]
    private string m_BGAName;
    [SerializeField]
    private float m_BGAVolume;
    [SerializeField]
    private bool m_Loop;
    [SerializeField]
    private AudioClip m_BGAClip;

    public string BGAName => m_BGAName;

    public void DoAudio()
    {
        if (m_BGAClip == null)
            return;

        AudioManager.scriptAudio.bgaSource.loop = m_Loop;
        AudioManager.scriptAudio.PlayBga(m_BGAClip, m_BGAVolume);
    }
}