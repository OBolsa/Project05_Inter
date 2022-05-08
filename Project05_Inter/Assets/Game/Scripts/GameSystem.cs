using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : GameStateMachine
{
    public PauseComponents pauseComponents;

    private void Start()
    {
        SetState(new Running(this));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            State.Pause();
        }
    }
}
