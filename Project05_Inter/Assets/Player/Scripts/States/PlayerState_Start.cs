using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Start : PlayerState
{   
    public PlayerState_Start(PlayerSystems system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        WaitForSeconds s = new WaitForSeconds(.5f);

        yield return s;

        Player.SetState(new PlayerState_Idle(Player));
    }
}