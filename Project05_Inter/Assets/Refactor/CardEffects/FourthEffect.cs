using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthEffect : MonoBehaviour, ICardEffects
{
    [SerializeField, Range(1, 5)] public int PointsForEachCombination;
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
        int combinationsPoints = 0;
        List<GameObject> cardsInPlayerBoard = new List<GameObject>();
        List<GameObject> cardsInEnemyBoard = new List<GameObject>();

        foreach (CardSpot c in gameBoard.PlayerSpots)
        {
            cardsInPlayerBoard.Add(c.cardInThisSpot);
        }

        foreach (CardSpot c in gameBoard.EnemySpots)
        {
            cardsInEnemyBoard.Add(c.cardInThisSpot);
        }

        //Check PlayerCombinations
        combinationsPoints += combinations.CheckCardsSequenceCombinations(cardsInPlayerBoard) > 0 ? 1 : 0;
        combinationsPoints += combinations.CheckCardsSuitsCombinations(cardsInPlayerBoard) > 0 ? 1 : 0;
        combinationsPoints += combinations.CheckCardsValueCombinations(cardsInPlayerBoard) > 0 ? 1 : 0;

        //CheckEnemyCombinations
        combinationsPoints += combinations.CheckCardsSequenceCombinations(cardsInEnemyBoard) > 0 ? 1 : 0;
        combinationsPoints += combinations.CheckCardsSuitsCombinations(cardsInEnemyBoard) > 0 ? 1 : 0;
        combinationsPoints += combinations.CheckCardsValueCombinations(cardsInEnemyBoard) > 0 ? 1 : 0;

        if (combinationsPoints > 0)
        {
            Debug.Log((isOnPlayerBoard ? "O Player" : "O Inimigo") + " conseguiu mais " + PointsForEachCombination + " por ter O Mago");

            if(isOnPlayerBoard)
                gameBoard.Match.AddPlayerPoints(combinationsPoints * PointsForEachCombination);
            else
                gameBoard.Match.AddEnemyPoints(combinationsPoints * PointsForEachCombination);
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
