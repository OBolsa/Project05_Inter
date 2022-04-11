using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDoubleEffect : MonoBehaviour, ICardEffects
{
    [HideInInspector] public CardConfig config;
    [HideInInspector] public Board gameBoard;
    [HideInInspector] public bool isOnPlayerBoard;

    public void OnEndGame()
    {
        throw new System.NotImplementedException();
    }

    public void OnEndTurn()
    {
        List<CardSystem> cardsInMyBoard = new List<CardSystem>();
        List<CardSystem> cardsClubsSuit = new List<CardSystem>();
        int mostValueCardIndex = 0;
        int pointsToAdd;
        bool haveCardWithThisSuit = false;

        foreach (CardSpot c in isOnPlayerBoard ? gameBoard.PlayerSpots : gameBoard.EnemySpots)
        {
            cardsInMyBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());

            if (cardsInMyBoard[cardsInMyBoard.Count - 1].Config.cardSuit == config.cardSuit)
            {
                cardsClubsSuit.Add(c.cardInThisSpot.GetComponent<CardSystem>());
                haveCardWithThisSuit = true;
            }
        }

        if (haveCardWithThisSuit)
        {
            for (int i = 0; i < cardsClubsSuit.Count; i++)
            {
                if (cardsClubsSuit[i].Config.cardValue > cardsClubsSuit[mostValueCardIndex].Config.cardValue)
                    mostValueCardIndex = i;
            }

            pointsToAdd = cardsClubsSuit[mostValueCardIndex].Config.cardValue;

            Debug.Log((isOnPlayerBoard ? "O Player" : "O Inimigo") + " conseguiu mais " + pointsToAdd + " por duplicar os pontos da carta de maior valor do naipe de " + config.cardSuit.ToString());

            if (isOnPlayerBoard)
                gameBoard.Match.AddPlayerPoints(pointsToAdd);
            else
                gameBoard.Match.AddEnemyPoints(pointsToAdd);
        }
    }

    public void OnPutCard()
    {
        gameBoard = FindObjectOfType<Board>();

        if(gameBoard.PlayerSpots[gameBoard.Match.Round] != null)
        {
            config = gameBoard.PlayerSpots[gameBoard.Match.Round].GetComponent<CardSystem>().Config;
            isOnPlayerBoard = true;
        }
        else
        {
            isOnPlayerBoard = false;
        }
    }
}
