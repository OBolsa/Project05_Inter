using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchState_EnemyTurn : MatchState
{
    public MatchState_EnemyTurn(MatchSystem system) : base(system)
    {
    }

    public override IEnumerator Start()
    {
        waitTime = new WaitForSeconds(0.9f);
        Debug.Log("_Enemy Turn");
        System.PlayerHand.canHighlightCard = false;
        System.DoFeedback("Turno inimigo", 1.2f);

        yield return waitTime;

        // Enemy Draw's a card;
        Debug.Log("_Enemy Draws a card");
        System.EnemyHand.DrawCard(System.MainDeck);

        yield return waitTime;

        // Enemy Discard's a card;
        Debug.Log("_Enemy Discard a card");
        System.EnemyHand.DiscardCard(System.EnemyHand.GetCardByImportance(false), System.DiscardDeck.transform);

        yield return waitTime;

        // Enemy PutDown a card;
        Debug.Log("_Enemy Put Down a Card");
        System.EnemyHand.PutCard(System.EnemyHand.GetCardByImportance(true), System.Board.EnemySpots[System.Turn]);

        yield return waitTime;

        System.SetState(new MatchState_MyTurn(System));
    }
}