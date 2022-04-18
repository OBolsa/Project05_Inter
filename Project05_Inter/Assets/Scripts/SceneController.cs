using UnityEngine.SceneManagement;

public class SceneController
{
    private static SceneController m_instance;

    public static SceneController Instance 
    {
        get
        {
            if(m_instance == null)
                m_instance = new SceneController();
            return m_instance;
        }
    }

    public void ChangeScene(string scene, LoadSceneMode mode)
    {
        if (scene == null)
            return;

        if(SceneManager.GetSceneByName(scene) != SceneManager.GetActiveScene())
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
