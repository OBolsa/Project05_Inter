using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSun : MonoBehaviour, ICardEffects
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
        List<CardSystem> enemyElegibleCards = new List<CardSystem>();
        int pointsToAdd = 0;

        foreach (CardSpot c in isOnPlayerBoard ? gameBoard.EnemySpots : gameBoard.PlayerSpots)
        {
            cardsInEnemyBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());

            if(c.cardInThisSpot.GetComponent<CardSystem>().Config.cardValue >= 3)
            {
                enemyElegibleCards.Add(c.cardInThisSpot.GetComponent<CardSystem>());
            }
        }

        if (enemyElegibleCards.Count > 0)
        {
            for (int i = 0; i < enemyElegibleCards.Count; i++)
            {
                pointsToAdd += enemyElegibleCards[i].Config.cardValue;
            }
        }

        Debug.Log((isOnPlayerBoard ? "O Player" : "O Inimigo") + " conseguiu mais " + pointsToAdd + " por ter O Carro");

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
