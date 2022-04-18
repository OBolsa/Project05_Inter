using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDefaultEffect : MonoBehaviour, ICardEffects
{
    [HideInInspector] public CardConfig config;
    [HideInInspector] public Board gameBoard;
    [HideInInspector] public bool isOnPlayerBoard;

    public void OnEndGame()
    {
        //
    }

    public void OnEndTurn()
    {
        //
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
