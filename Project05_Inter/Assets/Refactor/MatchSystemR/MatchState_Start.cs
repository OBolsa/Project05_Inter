using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchState_Start : MatchState
{
    public MatchState_Start(MatchSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        waitTime = new WaitForSeconds(1f);
        Debug.Log("_Match Start_");

        System.StartCoroutine(System.PlayerHand.FirstDraw());
        System.StartCoroutine(System.EnemyHand.FirstDraw());

        yield return waitTime;

        System.SetState(new MatchState_EnemyTurn(System));
    }
}