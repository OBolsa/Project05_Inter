using UnityEngine;

[System.Serializable]
public class BGMHandler
{
    [SerializeField]
    private string m_BGMName;
    [SerializeField]
    private float m_BGMVolume;
    [SerializeField]
    private bool m_Loop;
    [SerializeField]
    private AudioClip m_BGMClip;

    public string BGMName => m_BGMName;

    public void DoAudio()
    {
        if (m_BGMClip == null)
            return;

        AudioManager.scriptAudio.bgmSource.loop = m_Loop;
        AudioManager.scriptAudio.PlayBgm(m_BGMClip, m_BGMVolume);
    }
}