using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMChooseByCardImportance : MonoBehaviour, IMatchEnemyAI
{
    public void GiveCardValues(Hand hand)
    {
        foreach (GameObject g in hand.CardsInHand)
        {
            g.GetComponent<CardSystem>().ResetCardImportance();
        }

        for (int i = 1; i < 6; i++)
        {
            int numberOfCards = 0;

            for (int j = 0; j < hand.CardsInHand.Count; j++)
            {
                if(hand.CardsInHand[j].GetComponent<CardSystem>().Config.cardValue == i)
                {
                    numberOfCards += 1;
                }
            }

            foreach(GameObject g in hand.CardsInHand)
            {
                if(g.GetComponent<CardSystem>().Config.cardValue == i)
                {
                    g.GetComponent<CardSystem>().AddCardImportance(i + numberOfCards);
                }
            }
        }
    }
}
