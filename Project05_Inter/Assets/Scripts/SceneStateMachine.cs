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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Scene myScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(myScene);

        AudioListener audioL = FindObjectOfType<AudioListener>();
        EventSystem eventSystem = FindObjectOfType<EventSystem>();

        audioL.enabled = false;
        eventSystem.enabled = false;
    }

    public void ChangeScene(string scene)
    {
        AudioListener audioL = FindObjectOfType<AudioListener>();
        EventSystem eventSystem = FindObjectOfType<EventSystem>();

        audioL.enabled = true;
        eventSystem.enabled = true;

        if (SceneManager.GetActiveScene().name != "MatchScene")
            SceneController.Instance.ChangeScene(scene, LoadSceneMode.Single);
        else
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            //SceneController.Instance.ChangeScene("MatchScene", LoadSceneMode.Additive);
            //SceneManager.UnloadScene(SceneManager.GetActiveScene());
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