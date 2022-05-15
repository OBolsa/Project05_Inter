using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Walk : PlayerState
{
    public PlayerState_Walk(PlayerSystems system) : base(system)
    {
    }

    public override void Move()
    {
        Player.pMovement.DoMovement();
    }

    public override void Stop()
    {
        Player.SetState(new PlayerState_Idle(Player));
    }

    public override void Interact()
    {
        
    }
}