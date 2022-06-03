using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheTemperance : MonoBehaviour, ICardEffects
{
    [HideInInspector] public CardConfig config;
    [HideInInspector] public Board gameBoard;
    [HideInInspector] public bool isOnPlayerBoard;
    [SerializeField] private int pointsForThisCard;

    public void OnEndGame()
    {
        throw new System.NotImplementedException();
    }

    public void OnEndTurn()
    {
        List<CardSystem> cardsInEnemyBoard = new List<CardSystem>();
        List<CardSystem> enemyCards = new List<CardSystem>();
        int pointsToAdd = 0;

        foreach (CardSpot c in isOnPlayerBoard ? gameBoard.EnemySpots : gameBoard.PlayerSpots)
        {
            cardsInEnemyBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());

            if(c.cardInThisSpot.GetComponent<CardSystem>().Config.majorArcane)
            {
                enemyCards.Add(c.cardInThisSpot.GetComponent<CardSystem>());
            }
        }

        if (enemyCards.Count > 0)
        {
            for (int i = 0; i < enemyCards.Count; i++)
            {
                pointsToAdd += 2;
            }
        }

        pointsToAdd = pointsToAdd > 0 ? pointsToAdd + pointsForThisCard : 0;

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
