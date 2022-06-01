using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private string m_MenuState;
    [SerializeField]
    private MenuChannel m_MenuChannel;
    private string m_CurrentState;

    private void Start()
    {
        ChangeMenuState(m_MenuState);
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
}