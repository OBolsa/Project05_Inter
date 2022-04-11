using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : Hand
{
    [Header("Card Atributtes")]
    public bool canMoveCard;
    public List<Vector3> cardPositions;
    public IMatchEnemyAI EnemyAI;

    private void Awake()
    {
        EnemyAI = GetComponent<IMatchEnemyAI>();
        cardPositions = new List<Vector3>();
    }

    private void Update()
    {
        CardMovements();
    }

    #region Update Methods

    public void UpdateCardPosition()
    {
        UpdateVectorsPositions();

        for (int i = 0; i < CardsInHand.Count; i++)
        {
            CardsInHand[i].GetComponent<CardSystem>().StartCardMovement(cardPositions[i], Quaternion.Euler(new Vector3(20f, 0f, 0f)), 0.3f);
            CardsInHand[i].GetComponent<CardSystem>().cardCollider.enabled = true;
        }
    }

    public void UpdateCardPosition(int index)
    {
        UpdateVectorsPositions();

        CardsInHand[index].GetComponent<CardSystem>().StartCardMovement(cardPositions[index], Quaternion.Euler(new Vector3(20f, 0f, 0f)), 0.3f);
        CardsInHand[index].GetComponent<CardSystem>().cardCollider.enabled = true;
    }

    public void UpdateCardPosition(GameObject g)
    {
        UpdateVectorsPositions();

        g.GetComponent<CardSystem>().StartCardMovement(cardPositions[cardPositions.Count -1], Quaternion.Euler(new Vector3(20f, 0f, 0f)), 0.3f);
        g.GetComponent<CardSystem>().cardCollider.enabled = true;
    }

    public void CardMovements()
    {
        if (canMoveCard)
        {
            foreach (GameObject g in CardsInHand)
            {
                g.GetComponent<CardSystem>().MovementCard();
            }
        }
    }

    public void UpdateVectorsPositions()
    {
        cardPositions.Clear();

        float total = 0.8f * CardsInHand.Count;
        float firstPosition = 0.45f -(total/2);
        float xPos = firstPosition;

        for (int i = 0; i < CardsInHand.Count; i++)
        {
            var position = new Vector3(xPos, 0f + transform.position.y, 0f + transform.position.z);

            cardPositions.Add(position);

            xPos += 0.75f;
        }
    }
    #endregion

    #region Action Methods

    public override IEnumerator FirstDraw()
    {
        WaitTime = new WaitForSeconds(.1f);

        yield return WaitTime;

        while(CardsInHand.Count < Match.initialDraw)
        {
            DrawCard(MainDeck);

            yield return WaitTime;
        }

        EnemyAI.GiveCardValues(this);
    }

    public override void DrawCard(Deck deckUsed)
    {
        CardsInHand.Add(deckUsed.TopCard);
        deckUsed.DrawCard(transform);
        UpdateCardPosition();

        EnemyAI.GiveCardValues(this);
    }

    public int GetCardByImportance(bool getMoreImportants)
    {
        List<int> mostImportantCardsIndex = new List<int>();
        List<int> lessImportantCardsIndex = new List<int>();
        int passingScore = 0;

        for (int i = 0; i < CardsInHand.Count; i++)
        {
            if(CardsInHand[i].GetComponent<CardSystem>().CardImportance > passingScore)
            {
                passingScore = CardsInHand[i].GetComponent<CardSystem>().CardImportance;
            }
        }

        //passingScore /= 2;

        for (int i = 0; i < CardsInHand.Count; i++)
        {
            if (CardsInHand[i].GetComponent<CardSystem>().CardImportance >= passingScore)
                mostImportantCardsIndex.Add(i);
            else
                lessImportantCardsIndex.Add(i);
        }

        if (getMoreImportants)
        {
            Debug.Log(mostImportantCardsIndex.Count > 0 ? "Tem cartas importantes!" : "N tem cartas importantes");
            return mostImportantCardsIndex.Count > 0 ? mostImportantCardsIndex[Random.Range(0, mostImportantCardsIndex.Count - 1)] : Random.Range(0, CardsInHand.Count - 1);
        }
        else
        {
            Debug.Log(lessImportantCardsIndex.Count > 0 ? "Tem cartas inuteis!" : "N tem cartas inuteis");
            return lessImportantCardsIndex.Count > 0 ? lessImportantCardsIndex[Random.Range(0, lessImportantCardsIndex.Count - 1)] : Random.Range(0, CardsInHand.Count - 1);
        }
    }

    public override void DiscardCard(int cardIndex, Transform position)
    {
        DiscardDeck.AddCard(CardsInHand[cardIndex]);

        CardsInHand[cardIndex].transform.SetParent(position, true);
        CardsInHand.Remove(CardsInHand[cardIndex]);

        UpdateCardPosition();

        DiscardDeck.Card[0].GetComponent<CardSystem>().StartCardMovement(position.position, position.rotation, 0.3f);
    }

    public override void PutCard(int cardIndex, CardSpot spot)
    {
        spot.ReciveCard(CardsInHand[cardIndex]);

        CardsInHand[cardIndex].transform.SetParent(spot.transform, true);
        CardsInHand.Remove(CardsInHand[cardIndex]);

        UpdateCardPosition();

        spot.GetComponentInChildren<CardSystem>().StartCardMovement(spot.modelTransform.position, spot.modelTransform.rotation, 0.3f);

        if (spot.GetComponentInChildren<CardSystem>().Config.CardEffects != null)
            spot.GetComponentInChildren<CardSystem>().Config.CardEffects.OnPutCard();
    }

    public override void DiscardAllHand()
    {
        int hand = CardsInHand.Count;

        for (int i = 0; i < hand; i++)
        {
            DiscardCard(0, DiscardDeck.transform);
        }
    }
    #endregion
}