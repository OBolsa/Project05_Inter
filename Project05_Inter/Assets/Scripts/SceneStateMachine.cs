using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneStateMachine : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

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

    public void BackToGame()
    {
        ChangeScene("TestsScene01");
    }
}
