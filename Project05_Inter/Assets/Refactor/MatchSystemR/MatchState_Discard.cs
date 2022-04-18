using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchState_Discard : MatchState
{
    public MatchState_Discard(MatchSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        waitTime = new WaitForSeconds(0.3f);
        Debug.Log("_Discard");

        System.PlayerHand.canHighlightCard = true;
        System.DoFeedback("Discarte uma carta", 1f);

        return base.Start();
    }

    public override IEnumerator SelectCard()
    {
        waitTime = new WaitForSeconds(0.3f);

        System.PlayerHand.SelectCard();

        yield return waitTime;
    }

    public override IEnumerator Discard()
    {
        waitTime = new WaitForSeconds(0.3f);

        System.PlayerHand.canHighlightCard = false;
        System.PlayerHand.DiscardCard(System.PlayerHand._selectedCard, System.DiscardDeck.transform);

        yield return waitTime;

        System.PlayerHand.canHighlightCard = true;
        System.SetState(new MatchState_PutDown(System));
    }

    public override void CardOverlaySelection()
    {
        // Allow the card to overlay;
    }
}