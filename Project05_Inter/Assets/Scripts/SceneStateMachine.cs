using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneStateMachine : MonoBehaviour
{
    [SerializeField]
    private MatchChannel m_MatchChannel;
    [SerializeField]
    private UnityEvent m_OnSceneStart;
    [SerializeField]
    private UnityEvent m_OnSceneEnd;

    [SerializeField]
    private BGMHandler m_BGMHandler;
    [SerializeField]
    private BGAHandler m_BGAHandler;

    private void Awake()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
        m_MatchChannel.OnMatchStart += GoToMatch;
        m_MatchChannel.OnMatchEnd += BackToGame;
    }

    private void OnDestroy()
    {
        m_MatchChannel.OnMatchStart -= GoToMatch;
        m_MatchChannel.OnMatchEnd -= BackToGame;
    }

    private void Start()
    {
        m_OnSceneStart?.Invoke();

        m_BGMHandler.DoAudio();
        m_BGAHandler.DoAudio();
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoToMatch()
    {
        m_OnSceneEnd?.Invoke();
        SceneManager.LoadScene("MatchScene");
    }

    public void BackToGame()
    {
        m_OnSceneEnd?.Invoke();
        SceneManager.LoadScene("TestsScene01");
    }
}