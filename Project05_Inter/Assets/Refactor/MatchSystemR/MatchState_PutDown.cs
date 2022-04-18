using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchState_PutDown : MatchState
{
    public MatchState_PutDown(MatchSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        waitTime = new WaitForSeconds(0.3f);
        Debug.Log("_Put Down a Card");

        System.PlayerHand.canHighlightCard = true;
        System.DoFeedback("Coloque uma Carta em jogo", 1f);

        return base.Start();
    }

    public override IEnumerator SelectCard()
    {
        waitTime = new WaitForSeconds(0.3f);

        System.PlayerHand.SelectCard();

        yield return waitTime;
    }

    public override IEnumerator PutCard()
    {
        waitTime = new WaitForSeconds(0.3f);

        System.PlayerHand.canHighlightCard = false;

        // Put the Card;
        System.PlayerHand.PutCard(System.PlayerHand._selectedCard, System.Board.PlayerSpots[System.Turn]);

        yield return waitTime;

        System.NextTurn();
        System.CheckEndTurn();
    }

    public override void CardOverlaySelection()
    {
        // Allow the card to overlay;
    }
}