using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchState_MyTurn : MatchState
{
    public MatchState_MyTurn(MatchSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        waitTime = new WaitForSeconds(0.3f);
        Debug.Log("_My Turn");

        System.PlayerHand.canHighlightCard = false;
        System.DoFeedback("Compre uma carta", 1f);

        yield return waitTime;
    }

    public override IEnumerator DrawCard(Deck deck)
    {
        waitTime = new WaitForSeconds(0.3f);

        System.PlayerHand.DrawCard(deck);

        yield return waitTime;

        System.SetState(new MatchState_Discard(System));
    }
}