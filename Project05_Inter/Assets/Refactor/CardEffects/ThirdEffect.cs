using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEffect : MonoBehaviour, ICardEffects
{
    [SerializeField, Range(1, 5)] public int pointsDiscountForEachCard;
    [HideInInspector] public CardConfig config;
    [HideInInspector] public Board gameBoard;
    [HideInInspector] public bool isOnPlayerBoard;

    public void OnEndGame()
    {
        throw new System.NotImplementedException();
    }

    public void OnEndTurn()
    {
        List<CardSystem> playerCardsInBoard = new List<CardSystem>();
        List<CardSystem> enemyCardsInBoard = new List<CardSystem>();
        List<CardSystem> enemyMajorArcanesInBoard = new List<CardSystem>();

        int lessValuePlayerCardIndex = 0;
        int pointsToAdd = 0;
        int pointsToDiscount = 0;
        string debug = "";

        foreach (CardSpot c in gameBoard.PlayerSpots)
        {
            playerCardsInBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());
        }
        foreach (CardSpot c in gameBoard.EnemySpots)
        {
            enemyCardsInBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());
        }

        for (int i = 0; i < enemyCardsInBoard.Count; i++)
        {
            if (enemyCardsInBoard[i].Config.majorArcane)
            {
                enemyMajorArcanesInBoard.Add(enemyCardsInBoard[i]);
            }
        }

        for (int i = 0; i < playerCardsInBoard.Count; i++)
        {
            if (playerCardsInBoard[i].Config.cardValue < playerCardsInBoard[lessValuePlayerCardIndex].Config.cardValue && playerCardsInBoard[i].Config.cardValue > 0)
                lessValuePlayerCardIndex = i;
        }

        if(enemyMajorArcanesInBoard.Count > 0)
        {
            pointsToDiscount = -(enemyMajorArcanesInBoard.Count) * pointsDiscountForEachCard;
            pointsToAdd = playerCardsInBoard[lessValuePlayerCardIndex].Config.cardValue;
        }

        debug += (isOnPlayerBoard ? "E o Inimigo" : "E o Player") + " perdeu " + pointsToDiscount + " por culpa do O Mundo";

        Debug.Log(debug);

        if (isOnPlayerBoard)
        {
            gameBoard.Match.AddPlayerPoints(pointsToAdd);
            gameBoard.Match.AddEnemyPoints(pointsToDiscount);
        }
        else
        {
            gameBoard.Match.AddEnemyPoints(pointsToAdd);
            gameBoard.Match.AddPlayerPoints(pointsToDiscount);
        }
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
