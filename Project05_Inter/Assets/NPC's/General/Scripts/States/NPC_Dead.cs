using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dead : NPC_State
{
    public NPC_Dead(NPCSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        NPC.interactionState = 2;
        NPC.canvasManager.UpdateDisplay();
        return base.Start();
    }

    public override void Move()
    {
    }

    public override void Stop()
    {
    }
}