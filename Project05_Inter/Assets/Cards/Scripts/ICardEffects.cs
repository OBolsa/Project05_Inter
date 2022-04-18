using UnityEngine;

public interface ICardEffects
{
    void OnPutCard();
    void OnEndTurn();
    void OnEndGame();
    public Board GetGameBoard();
}