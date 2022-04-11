using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecksCombinations : MonoBehaviour, ICombinations
{
    public int pointsForPairs;
    public int pointsForThreeOfAKind;
    public int pointsForFourOfAKind;
    public int pointsForTwoPair;
    public int pointsForFullHouse;
    public int pointsForFlush;
    public int pointsForSequence;
    public int pointsForRoyalFlush;

    public int CheckCardsValueCombinations(List<GameObject> cards)
    {
        bool pair = false;
        bool threeOfAKind = false;
        bool fourOfAKind = false;
        bool twoPair = false;
        bool fullHouse = false;

        List<CardSystem> cardsInfo = new List<CardSystem>();

        foreach(GameObject g in cards)
        {
            cardsInfo.Add(g.GetComponent<CardSystem>());
        }

        for (int i = 1; i < 6; i++)
        {
            int numberOfCards = 0;

            for (int j = 0; j < cardsInfo.Count; j++)
            {

                if(cardsInfo[j].Config.cardValue == i)
                {
                    numberOfCards += 1;
                }
            }

            switch (numberOfCards)
            {
                default:
                    break;
                case 2:
                    if(threeOfAKind == true)
                    {
                        pair = false;
                        threeOfAKind = false;
                        fullHouse = true;
                    }
                    else if (pair == false)
                    {
                        pair = true;
                        twoPair = false;
                    }
                    else
                    {
                        twoPair = true;
                        pair = false;
                    }
                    break;
                case 3:
                    if(pair == false)
                    {
                        threeOfAKind = true;
                        pair = false;
                        fullHouse = false;
                    }
                    else
                    {
                        threeOfAKind = false;
                        pair = false;
                        fullHouse = true;
                    }
                    break;
                case 4:
                    pair = false;
                    threeOfAKind = false;
                    fullHouse = false;
                    twoPair = false;
                    fourOfAKind = true;
                    break;
            }
        }

        string debug = "Pair: " + pair + "\n";
        debug += "Three of a Kind: " + threeOfAKind + "\n";
        debug += "Four of a Kind: " + fourOfAKind + "\n";
        debug += "Two Pairs: " + twoPair + "\n";
        debug += "Full House: " + fullHouse + "\n";

        Debug.Log(debug);

        if (pair) return pointsForPairs;
        else if (threeOfAKind) return pointsForThreeOfAKind;
        else if (fourOfAKind) return pointsForFourOfAKind;
        else if (twoPair) return pointsForTwoPair;
        else if (fullHouse) return pointsForFullHouse;
        else return 0;
    }

    public int CheckCardsSuitsCombinations(List<GameObject> cards)
    {
        List<CardSystem> cardsInfo = new List<CardSystem>();

        foreach(GameObject g in cards)
        {
            cardsInfo.Add(g.GetComponent<CardSystem>());
        }

        if (CheckSuits(CardSuits.Clubs, cardsInfo) || CheckSuits(CardSuits.Diamonds, cardsInfo) || CheckSuits(CardSuits.Hearts, cardsInfo) || CheckSuits(CardSuits.Spades, cardsInfo))
        {
            Debug.Log("Have Flush!");
            return pointsForFlush;
        }
        else
            return 0;
    }

    public int CheckCardsSequenceCombinations(List<GameObject> cards)
    {
        bool haveSequence = true;
        List<CardSystem> cardsInfo = new List<CardSystem>();

        foreach (GameObject g in cards)
        {
            cardsInfo.Add(g.GetComponent<CardSystem>());
        }

        for (int i = 0; i < cardsInfo.Count; i++)
        {
            if (cardsInfo[i].Config.cardValue != i + 1)
            {
                haveSequence = false;
                break;
            }
        }

        //if (!haveSequence)
        //{
        //    bool haveInvertedSequence = true;
        //    int j = 0;

        //    for (int i = 5; i > -1; i--)
        //    {
        //        if (cardsInfo[j].Config.cardValue != i)
        //        {
        //            haveInvertedSequence = false;
        //            break;
        //        }

        //        j += 1;
        //    }

        //    if (haveInvertedSequence)
        //    {
        //        haveSequence = true;
        //    }
        //}

        if (haveSequence)
        {
            if (CheckSuits(CardSuits.Clubs, cardsInfo) || CheckSuits(CardSuits.Diamonds, cardsInfo) || CheckSuits(CardSuits.Hearts, cardsInfo) || CheckSuits(CardSuits.Spades, cardsInfo))
            {
                Debug.Log("Have Royal Flush!");
                return pointsForRoyalFlush;
            }
            else
            {
                Debug.Log("Have Sequence!");
                return pointsForSequence;
            }
        }
        else
            return 0;
    }

    public int CheckCombinationFour(List<GameObject> cards)
    {
        
        return 0;
    }

    public int CheckCombinationFive(List<GameObject> cards)
    {
        
        return 0;
    }

    public bool CheckSuits(CardSuits suit, List<CardSystem> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if(cards[i].Config.cardSuit != suit)
            {
                return false;
            }
        }

        return true;
    }
}
