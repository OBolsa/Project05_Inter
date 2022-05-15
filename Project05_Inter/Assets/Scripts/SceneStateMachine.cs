using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Overworld,
    Match,
    Menu
}

public class SceneStateMachine : MonoBehaviour
{
    [SerializeField]
    private MatchChannel m_MatchChannel;
    [SerializeField]
    private GameStates m_GameState;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        m_MatchChannel.OnMatchStart += GoToMatch;
        m_MatchChannel.OnMatchEnd += BackToGame;
    }

    private void OnDestroy()
    {
        m_MatchChannel.OnMatchStart -= GoToMatch;
        m_MatchChannel.OnMatchEnd -= BackToGame;
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

    public void GoToMatch()
    {
        switch (m_GameState)
        {
            case GameStates.Overworld:
                ChangeScene("MatchScene");
                break;
        }
    }

    public void BackToGame()
    {
        switch (m_GameState)
        {
            case GameStates.Match:
                ChangeScene("TestsScene01");
                break;
        }
    }
}