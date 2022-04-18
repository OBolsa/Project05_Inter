using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePunisherEffect : MonoBehaviour, ICardEffects
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
        List<CardSystem> cardsInEnemyBoard = new List<CardSystem>();
        List<CardSystem> differentCardSuit = new List<CardSystem>();
        int pointsToAdd = 0;

        foreach (CardSpot c in isOnPlayerBoard ? gameBoard.EnemySpots : gameBoard.PlayerSpots)
        {
            cardsInEnemyBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());

            if(c.cardInThisSpot.GetComponent<CardSystem>().Config.cardSuit != config.cardSuit)
            {
                differentCardSuit.Add(c.cardInThisSpot.GetComponent<CardSystem>());
            }
        }

        if (differentCardSuit.Count > 0)
            pointsToAdd -= differentCardSuit.Count;

        Debug.Log((isOnPlayerBoard ? "O Inimigo" : "O Player") + " perdeu " + pointsToAdd + " por culpa do O Julgamento");

        if (!isOnPlayerBoard)
            gameBoard.Match.AddPlayerPoints(pointsToAdd);
        else
            gameBoard.Match.AddEnemyPoints(pointsToAdd);
    }

    public void OnPutCard()
    {
        gameBoard = FindObjectOfType<Board>();

        if (gameBoard.PlayerSpots[gameBoard.Match.Turn].cardInThisSpot != null)
        {
            config = gameBoard.PlayerSpots[gameBoard.Match.Turn].cardInThisSpot.GetComponent<CardSystem>().Config;
            isOnPlayerBoard = true;
        }
        else
        {
            config = gameBoard.EnemySpots[gameBoard.Match.Turn].cardInThisSpot.GetComponent<CardSystem>().Config;
            isOnPlayerBoard = false;
        }
    }

    public Board GetGameBoard()
    {
        return gameBoard;
    }
}
