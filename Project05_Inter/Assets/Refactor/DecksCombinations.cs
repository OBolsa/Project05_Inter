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

    public int CheckCombinationOne(List<GameObject> cards)
    {
        bool pair = false;
        bool threeOfAKind = false;
        bool fourOfAKind = false;
        bool twoPair = false;
        bool fullHouse = false;

        List<CardSystem> configs = new List<CardSystem>();

        foreach(GameObject g in cards)
        {
            configs.Add(g.GetComponent<CardSystem>());
        }

        for (int i = 1; i < 6; i++)
        {
            int numberOfCards = 0;

            for (int j = 0; j < configs.Count; j++)
            {

                if(configs[j].Config.cardValue == i)
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

    public int CheckCombinationTwo(List<GameObject> cards)
    {
        
        return 0;
    }

    public int CheckCombinationThree(List<GameObject> cards)
    {
        
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
}
