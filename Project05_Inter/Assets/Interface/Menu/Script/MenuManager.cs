using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private string m_MenuState;
    [SerializeField]
    private MenuChannel m_MenuChannel;
    private string m_CurrentState;

    [SerializeField]
    private SFXHandler[] m_SFXs;
    private Dictionary<string, SFXHandler> m_SFXList = new Dictionary<string, SFXHandler>();

    private void Start()
    {
        ChangeMenuState(m_MenuState);

        if (m_SFXs.Length > 0)
        {
            foreach (var sfx in m_SFXs)
            {
                m_SFXList.Add(sfx.SFXName, sfx);
            }
        }
    }

    private void Update()
    {
        if(m_SFXs.Length > 0)
        {
            foreach (var sfx in m_SFXs)
            {
                sfx.DoAudioCooldown();
            }
        }
    }

    public void ChangeMenuState(string state)
    {
        m_MenuChannel.RaiseMenuState(state);
        m_MenuState = state;
    }

    public void GoToScene(string scene)
    {
        StartCoroutine(GoToSceneCoroutine(scene));
    }

    public void SetSceneState(string state)
    {
        m_MenuChannel.RaiseMenuState(state);
    }

    private IEnumerator GoToSceneCoroutine(string scene)
    {
        m_MenuChannel.RaiseMenuState("Game");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySFX(string sfx)
    {
        if(m_SFXList.ContainsKey(sfx))
            m_SFXList[sfx].DoAudio(Vector2.one);
    }
}