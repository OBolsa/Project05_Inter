using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Important Atributtes")]
    public MatchSystem Match;
    public ToolTipBoxSystem ToolTip;

    [Header("Spots")]
    public List<CardSpot> PlayerSpots;
    public List<CardSpot> EnemySpots;
    public List<string> PlayerPointsText;
    public List<string> EnemyPointsText;

    #region Update Methods

    private void Start()
    {
        FillMySpots();
    }

    [ContextMenu("Fill Spots")]
    public void FillMySpots()
    {
        PlayerSpots = new List<CardSpot>();

        if (PlayerSpots.Count > 0)
            PlayerSpots.Clear();

        if (EnemySpots.Count > 0)
            EnemySpots.Clear();

        var childs = GetComponentsInChildren<CardSpot>();

        foreach(CardSpot c in childs)
        {
            if (c.playerSpot)
                PlayerSpots.Add(c);
            else
                EnemySpots.Add(c);
        }
    }

    public void DiscardSpots()
    {
        for (int i = 0; i < PlayerSpots.Count; i++)
        {
            if(PlayerSpots[i].cardInThisSpot != null)
            {
                Match.DiscardDeck.AddCard(PlayerSpots[i].cardInThisSpot);
                PlayerSpots[i].cardInThisSpot.transform.SetParent(Match.DiscardDeck.transform, true);
                PlayerSpots[i].cardInThisSpot.GetComponent<CardSystem>().StartCardMovement(Match.DiscardDeck.transform.position, Match.DiscardDeck.transform.rotation, 0.3f);
                PlayerSpots[i].cardInThisSpot = null;
            }

            if (EnemySpots[i].cardInThisSpot != null)
            {
                Match.DiscardDeck.AddCard(EnemySpots[i].cardInThisSpot);
                EnemySpots[i].cardInThisSpot.transform.SetParent(Match.DiscardDeck.transform, true);
                EnemySpots[i].cardInThisSpot.GetComponent<CardSystem>().StartCardMovement(Match.DiscardDeck.transform.position, Match.DiscardDeck.transform.rotation, 0.3f);
                EnemySpots[i].cardInThisSpot = null;
            }
        }
    }

    #endregion
}
