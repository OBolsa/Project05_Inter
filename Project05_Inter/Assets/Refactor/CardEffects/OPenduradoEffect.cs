using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPenduradoEffect : MonoBehaviour, ICardEffects
{
    [SerializeField, Range(1, 5)] public int pointsForThisCardEffectAlone;
    [HideInInspector] public CardConfig config;
    [HideInInspector] public Board gameBoard;
    [HideInInspector] public bool isOnPlayerBoard;

    public void OnEndGame()
    {
        throw new System.NotImplementedException();
    }

    public void OnEndTurn()
    {
        List<CardSystem> cardsInPlayerBoard = new List<CardSystem>();
        int pointsToAdd = 0;

        foreach (CardSpot c in isOnPlayerBoard ? gameBoard.PlayerSpots : gameBoard.EnemySpots)
        {
            cardsInPlayerBoard.Add(c.cardInThisSpot.GetComponent<CardSystem>());
        }

        for (int i = 0; i < cardsInPlayerBoard.Count; i++)
        {
            pointsToAdd += cardsInPlayerBoard[i].Config.cardValue switch
            {
                2 => 2,
                3 => 0,
                4 => -2,
                5 => -4,
                _ => 0,
            };
        }

        pointsToAdd = pointsToAdd > 0 ? 0 : pointsToAdd + pointsForThisCardEffectAlone;

        if (pointsToAdd > 0)
        {
            Debug.Log((isOnPlayerBoard ? "O Player" : "O Inimigo") + " conseguiu mais " + pointsToAdd + " por ter O Pendurado");

            if (isOnPlayerBoard)
                gameBoard.Match.AddPlayerPoints(pointsToAdd);
            else
                gameBoard.Match.AddEnemyPoints(pointsToAdd);
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
