using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSuits
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

[CreateAssetMenu(fileName = "Card_Name_Suit", menuName = "Scriptable Objects/Card")]
public class CardConfig : ScriptableObject
{
    [Header("Card Main Atributtes")]
    public string cardName;
    [TextArea(0, 5)] public string cardDescription;
    public Sprite cardImage;

    [Header("Card Values Atributtes")]
    public int cardValue;
    public bool majorArcane;
    public CardSuits cardSuit;

    [Header("Card Effects")]
    [SerializeField] private GameObject effects;
    private ICardEffects _cardEffects;

    public ICardEffects CardEffects 
    {
        get 
        {
            if (effects != null)
            {
                _cardEffects = effects.GetComponent<ICardEffects>();
                return _cardEffects;
            }
            else
                return null;
        }
    }
}
