using UnityEngine;

[System.Serializable]
public class SFXHandler
{
    [SerializeField]
    private string m_SFXName;
    [SerializeField]
    private float m_SFXVolume;
    [SerializeField]
    private float m_SFXCooldown;
    [SerializeField]
    private AudioClip m_SFXClip;
    private bool isSFXCooldown;
    private float SFXCooldownCount;

    public string SFXName => m_SFXName;

    public void DoAudioCooldown()
    {
        if (isSFXCooldown)
        {
            SFXCooldownCount += Time.deltaTime;

            if (SFXCooldownCount >= m_SFXCooldown)
            {
                isSFXCooldown = false;
            }
        }
    }

    public void DoAudio()
    {
        if (!isSFXCooldown)
        {
            SFXCooldownCount = 0f;
            AudioManager.scriptAudio.PlaySfxSimple(m_SFXClip);
            isSFXCooldown = true;
        }
    }

    public void DoAudio(Vector2 pitchVariance)
    {
        if (!isSFXCooldown)
        {
            SFXCooldownCount = 0f;
            AudioManager.scriptAudio.PlaySfx(m_SFXClip, m_SFXVolume, pitchVariance, AudioManager.scriptAudio.sfxSource);
            isSFXCooldown = true;
        }
    }
}