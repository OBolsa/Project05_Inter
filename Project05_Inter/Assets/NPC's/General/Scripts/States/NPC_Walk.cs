using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Walk : NPC_State
{
    public NPC_Walk(NPCSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        Debug.Log("NPC " + NPC.configs.npcName + " is Walking.");
        return base.Start();
    }

    public override void Move()
    {
        NPC.movement.DoMovement(NPC.speed);
    }

    public override void Stop()
    {
        NPC.SetState(new NPC_Wait(NPC));
    }
}
