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
    static List<Card> selectedCards = new List<Card>();

    public CardColor color;
    public CardType type;
    public Sprite baseSprite = null;
    public Vector3 scaleToShrink;
    [HideInInspector]
    public bool isSelected = false;

    Vector3 defaultScale;

    public static List<Card> SelectedCards { get => selectedCards; }

    public Card(CardColor color, CardType type)
    {
        this.color = color;
        this.type = type;
    }

    public Card() { }

    public static void AddCardToSelected(Card card)
    {
        selectedCards.Add(card);

        //Highlite card
        card.GetComponent<SpriteRenderer>().color = new Color32(156, 156, 156, 255);
    }

    public static void RemoveCardFromSelected(Card card)
    {
        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);

            //Remove highlite
            card.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public static void ClearSelectedCards()
    {
        foreach (var item in selectedCards)
        {
            item.GetComponent<SpriteRenderer>().color = Color.white;
        }
        selectedCards.Clear();
    }

    private void Start()
    {
        defaultScale = transform.localScale;
        scaleToShrink = defaultScale;
    }

    private void Update()
    {
        if (transform.localScale != scaleToShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleToShrink, 0.5f);
        }
    }

    public void RevealCard()
    {
        if (GetComponent<SpriteRenderer>().sprite != baseSprite && baseSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = baseSprite;
        }
    }
}
