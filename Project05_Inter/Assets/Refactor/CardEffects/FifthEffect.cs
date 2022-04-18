using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthEffect : MonoBehaviour, ICardEffects
{
    [HideInInspector] public CardConfig config;
    [HideInInspector] public Board gameBoard;
    [HideInInspector] public ICombinations combinations;
    [HideInInspector] public bool isOnPlayerBoard;

    public void OnEndGame()
    {
        throw new System.NotImplementedException();
    }

    public void OnEndTurn()
    {
        List<CardSystem> cardsInPlayerBoard = new List<CardSystem>();
        List<CardSystem> cardsInEnemyBoard = new List<CardSystem>();
        int pointsToAdd = 0;
        int pointsToDiscount = 0;
        int mostValueEnemyCardIndex = 0;
        int lessValuePlayerCardIndex = 0;

        foreach (CardSpot c in isOnPlayerBoard ? gameBoard.PlayerSpots : gameBoard.EnemySpots)
        {
            cardsInPlayerBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());
        }

        foreach (CardSpot c in isOnPlayerBoard ? gameBoard.EnemySpots : gameBoard.PlayerSpots)
        {
            cardsInEnemyBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());
        }

        for (int i = 0; i < cardsInPlayerBoard.Count; i++)
        {
            if(cardsInPlayerBoard[i].Config.cardValue < cardsInPlayerBoard[lessValuePlayerCardIndex].Config.cardValue)
            {
                lessValuePlayerCardIndex = i;
            }
        }

        for (int i = 0; i < cardsInEnemyBoard.Count; i++)
        {
            if (cardsInEnemyBoard[i].Config.cardValue > cardsInEnemyBoard[mostValueEnemyCardIndex].Config.cardValue)
            {
                mostValueEnemyCardIndex = i;
            }
        }

        pointsToAdd = cardsInPlayerBoard[lessValuePlayerCardIndex].Config.cardValue - cardsInEnemyBoard[mostValueEnemyCardIndex].Config.cardValue;
        pointsToDiscount = cardsInEnemyBoard[mostValueEnemyCardIndex].Config.cardValue - cardsInPlayerBoard[lessValuePlayerCardIndex].Config.cardValue;

        if (pointsToAdd > 0)
        {
            string debug = "";
            debug += (isOnPlayerBoard ? "O Player" : "O Inimigo") + " conseguiu mais " + pointsToAdd + " roubados do " + (isOnPlayerBoard ? "O Inimigo" : "O Player") + "\n";
            debug += (isOnPlayerBoard ? "O Inimigo" : "O Player") + " perdeu " + pointsToDiscount + " do roubo do " + (isOnPlayerBoard ? "O Player" : "O Inimigo");
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
