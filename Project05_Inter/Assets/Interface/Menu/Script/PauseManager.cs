using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private MenuChannel m_MenuChannel;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DoPause();
        }
    }

    public void DoPause()
    {
        isPaused = !isPaused;

        m_MenuChannel.RaiseMenuState(isPaused ? "Game" : "Pause");
    }
}
