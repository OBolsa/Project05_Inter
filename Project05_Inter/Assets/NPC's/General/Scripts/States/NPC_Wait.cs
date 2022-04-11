using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Wait : NPC_State
{
    public NPC_Wait(NPCSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        Debug.Log("NPC " + NPC.configs.npcName + " is Waiting.");
        return base.Start();
    }

    public override void Move()
    {
        NPC.SetState(new NPC_Walk(NPC));
    }

    public override void Stop()
    {
    }
}