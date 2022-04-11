using System.Collections;
using UnityEngine;

public class PlayerState_Idle : PlayerState
{
    public PlayerState_Idle(PlayerSystems system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        yield break;
    }

    public override void Move()
    {
        Player.SetState(new PlayerState_Walk(Player));
    }

    public override void Interact()
    {
        Player.DialogueController();
    }
}