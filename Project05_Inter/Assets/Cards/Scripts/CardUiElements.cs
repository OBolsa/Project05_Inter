using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUiElements : MonoBehaviour
{
    [Header("External Scripts")]
    [SerializeField] private CardSystem card;

    [Header("Default Configs")]
    [SerializeField] public Sprite[] suitsSprites;

    [Header("Card Elements")]
    [SerializeField] public TMP_Text cardName;
    [SerializeField] public TMP_Text number1;
    [SerializeField] public TMP_Text number2;
    [SerializeField] public Image suit1;
    [SerializeField] public Image suit2;
    [SerializeField] public Image cardImage;

    private void Start()
    {
        //UpdateElements();
    }

    public void UpdateElements()
    {
        cardName.text = card.Config.cardName;

        if(card.Config.cardValue > 0)
        {
            number1.text = card.Config.cardValue.ToString();
            number2.text = card.Config.cardValue.ToString();
        }
        else
        {
            number1.text = "";
            number2.text = "";
        }

        switch (card.Config.cardSuit)
        {
            default:
            case CardSuits.Clubs:
                suit1.sprite = suitsSprites[0];
                suit2.sprite = suitsSprites[0];
                break;

            case CardSuits.Diamonds:
                suit1.sprite = suitsSprites[1];
                suit2.sprite = suitsSprites[1];
                break;

            case CardSuits.Hearts:
                suit1.sprite = suitsSprites[2];
                suit2.sprite = suitsSprites[2];
                break;

            case CardSuits.Spades:
                suit1.sprite = suitsSprites[3];
                suit2.sprite = suitsSprites[3];
                break;
        }
    }
}