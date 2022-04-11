using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC_State
{
    protected NPCSystem NPC;

    public NPC_State(NPCSystem system)
    {
        NPC = system;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual void Move()
    {

    }

    public virtual void Stop()
    {

    }
}
