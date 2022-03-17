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

[CreateAssetMenu(fileName = "Card_Name_Suit", menuName = "Scriptable Object/Create New/Card")]
public class CardConfig : ScriptableObject
{
    [Header("Card Main Atributtes")]
    public string cardName;
    [TextArea(0, 5)] public string cardDescription;
    public Sprite cardImage;

    [Header("Card Values Atributtes")]
    public int cardValue;
    public CardSuits cardSuit;

    [Header("Card Effects")]
    [SerializeField] private Object effects;

    public ICardEffects CardEffects { get { return (ICardEffects)effects; }}
}
