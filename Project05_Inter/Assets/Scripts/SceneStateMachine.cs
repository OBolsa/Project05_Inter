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
        SceneManager.LoadScene("GameScene");
    }
}