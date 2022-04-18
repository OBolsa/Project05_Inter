using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHighlight : MonoBehaviour, IHighlightSelection
{
    [Range(1, 2)] public float scaleFactor;
    public Vector3 startScale;
    private Vector3 newScale;
    private bool isHighlighed;
    public Canvas Layer;

    public void HighlightInitialize(Transform selection)
    {
        startScale = selection.localScale;
    }

    public void OnHighlight(Transform selection)
    {
        CardSystem card = GetComponent<CardSystem>();


        newScale = startScale * scaleFactor;

        if (!isHighlighed)
        {
            card.gameBoard.ToolTip.StopAllCoroutines();
            card.gameBoard.ToolTip.ShowToolTip(card.Config.cardName, card.Config.cardSuit.ToString(), card.Config.cardDescription, true);

            Layer.sortingOrder = 1;
            selection.transform.localScale = newScale;
        }
        isHighlighed = true;
    }

    public void OnDehighlight(Transform selection)
    {
        CardSystem card = GetComponent<CardSystem>();


        newScale = startScale;

        if (isHighlighed)
        {
            card.gameBoard.ToolTip.StopAllCoroutines();
            card.gameBoard.ToolTip.ShowToolTip(card.Config.cardName, card.Config.cardSuit.ToString(), card.Config.cardDescription, false);

            Layer.sortingOrder = 0;
            selection.transform.localScale = newScale;
        }
        isHighlighed = false;
    }
}