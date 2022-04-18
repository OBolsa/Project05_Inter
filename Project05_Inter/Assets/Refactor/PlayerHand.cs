using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : Hand
{
    [Header("External Atributtes")]
    public Transform _SelectCardPosition;
    public GameObject _selectedCard;
    public Board board;

    [Header("Card Atributtes")]
    public bool canMoveCard;
    public bool canHighlightCard;
    public List<Vector3> cardPositions;

    private void Awake()
    {
        CardsInHand = new List<GameObject>();
        cardPositions = new List<Vector3>();
    }

    private void Update()
    {
        CardHighlight();
        CardMovements();
    }

    #region Update Methods

    public void UpdateCardPosition()
    {
        UpdateVectorsPositions();

        for (int i = 0; i < CardsInHand.Count; i++)
        {
            CardsInHand[i].GetComponent<CardSystem>().StartCardMovement(cardPositions[i], Quaternion.Euler(new Vector3(-20f, 0f, 0f)), 0.3f);
            CardsInHand[i].GetComponent<CardSystem>().cardCollider.enabled = true;
        }
    }

    public void UpdateCardPosition(int index)
    {
        UpdateVectorsPositions();

        CardsInHand[index].GetComponent<CardSystem>().StartCardMovement(cardPositions[index], Quaternion.Euler(new Vector3(-20f, 0f, 0f)), 0.3f);
        CardsInHand[index].GetComponent<CardSystem>().cardCollider.enabled = true;
    }

    public void UpdateCardPosition(GameObject g)
    {
        UpdateVectorsPositions();

        g.GetComponent<CardSystem>().StartCardMovement(cardPositions[cardPositions.Count -1], Quaternion.Euler(new Vector3(-20f, 0f, 0f)), 0.3f);
        g.GetComponent<CardSystem>().cardCollider.enabled = true;
    }

    public void CardHighlight()
    {
        if (canHighlightCard)
        {
            foreach (GameObject g in CardsInHand)
            {
                g.GetComponent<CardSystem>().CardHighlight();
            }
        }
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
    }

    public override void DrawCard(Deck deck)
    {
        CardsInHand.Add(deck.TopCard);
        deck.DrawCard(transform);
        CardsInHand[CardsInHand.Count - 1].GetComponent<CardSystem>().gameBoard = board;
        UpdateCardPosition();
    }

    public override void DiscardCard(GameObject card, Transform position)
    {
        DiscardDeck.AddCard(card);

        card.transform.SetParent(position, true);
        CardsInHand.Remove(card);

        UpdateCardPosition();

        DiscardDeck.Card[0].GetComponent<CardSystem>().StartCardMovement(position.position, position.rotation, 0.3f);

        _selectedCard = null;
    }

    public override void SelectCard()
    {
        var colisor = MouseSelector.HitCollider();
        var selectedCardSystem = colisor.GetComponentInParent<CardSystem>();

        if (_selectedCard != null)
        {
            _selectedCard.GetComponent<CardSystem>().UnSelect = true;
            _selectedCard.GetComponent<CardSystem>().IsSelected = false;
        }

        if (selectedCardSystem != null && selectedCardSystem != _selectedCard)
        {
            if (!selectedCardSystem.IsInMovement)
            {
                _selectedCard = selectedCardSystem.gameObject;
                _selectedCard.GetComponent<CardSystem>().Select = true;
                _selectedCard.GetComponent<CardSystem>().IsSelected = true;
                _selectedCard.GetComponent<CardSystem>().StartCardMovement(_SelectCardPosition.position, _SelectCardPosition.rotation, 0.3f);
            }
        }
        else
        {
            _selectedCard = null;
        }
    }

    public override void PutCard(GameObject card, CardSpot spot)
    {
        spot.ReciveCard(card);

        card.transform.SetParent(spot.transform, true);
        CardsInHand.Remove(card);

        UpdateCardPosition();

        spot.GetComponentInChildren<CardSystem>().StartCardMovement(spot.modelTransform.position, spot.modelTransform.rotation, 0.3f);

        if (spot.GetComponentInChildren<CardSystem>().Config.CardEffects != null)
            spot.GetComponentInChildren<CardSystem>().Config.CardEffects.OnPutCard();

        _selectedCard = null;
    }

    public override void DiscardAllHand()
    {
        int hand = CardsInHand.Count;

        for (int i = 0; i < hand; i++)
        {
            DiscardCard(CardsInHand[0], DiscardDeck.transform);
        }
    }
    #endregion
}