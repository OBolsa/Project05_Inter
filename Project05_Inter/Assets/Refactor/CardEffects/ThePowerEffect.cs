using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePowerEffect : MonoBehaviour, ICardEffects
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
        List<CardSystem> cardsWithThisSuit = new List<CardSystem>();

        int pointsToAdd = 0;
        bool haveThisSuitCard = false;

        foreach (CardSpot c in isOnPlayerBoard ? gameBoard.PlayerSpots : gameBoard.EnemySpots)
        {
            cardsInMyBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());

            if (cardsInMyBoard[cardsInMyBoard.Count - 1].Config.cardSuit == config.cardSuit)
            {
                cardsWithThisSuit.Add(c.cardInThisSpot.GetComponent<CardSystem>());
                haveThisSuitCard = true;
            }
        }

        if (haveThisSuitCard)
        {
            pointsToAdd += cardsWithThisSuit.Count;
            Debug.Log((isOnPlayerBoard ? "O Player" : "O Inimigo") + " conseguiu mais " + pointsToAdd + " por empoderar os " + config.cardSuit.ToString());
        }


        if (isOnPlayerBoard)
            gameBoard.Match.AddPlayerPoints(pointsToAdd);
        else
            gameBoard.Match.AddEnemyPoints(pointsToAdd);
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
