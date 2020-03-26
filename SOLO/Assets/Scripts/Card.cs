using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CardColor
{
    Red, Green, Yellow, Blue, Black
}

public enum CardType
{
    One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Skipp, Switch_direction, Change_cards, Switch_cards_all, Change_color, Draw_2, Draw_4
}

public class Card : MonoBehaviour
{
    public CardColor color;
    public CardType type;
    public Sprite baseSprite = null;

    public Card(CardColor color, CardType type)
    {
        this.color = color;
        this.type = type;
    }

    public Card() { }

    public void RevealCard()
    {
        if (baseSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = baseSprite;
        }
    }
}
