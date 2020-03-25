using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPackController : MonoBehaviour
{
    const ushort CARDS_MAX_NUMBER = 112;

    public HandController playerHand;
    public List<GameObject> blueCardsPrefabs;
    public List<GameObject> redCardsPrefabs;
    public List<GameObject> greenCardsPrefabs;
    public List<GameObject> yellowCardsPrefabs;

    List<Card> cards = new List<Card>();

    private void Start()
    {
        CardColor color = CardColor.Blue;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                cards.Add(new Card(color, CardType.One));
                cards.Add(new Card(color, CardType.Two));
                cards.Add(new Card(color, CardType.Three));
                cards.Add(new Card(color, CardType.Four));
                cards.Add(new Card(color, CardType.Five));
                cards.Add(new Card(color, CardType.Six));
                cards.Add(new Card(color, CardType.Seven));
                cards.Add(new Card(color, CardType.Eight));
                cards.Add(new Card(color, CardType.Nine));
                cards.Add(new Card(color, CardType.Skipp));
                cards.Add(new Card(color, CardType.Switch_direction));
                cards.Add(new Card(color, CardType.Draw_2));
                cards.Add(new Card(color, CardType.Change_cards));
            }

            switch (i)
            {
                case 0:
                    color = CardColor.Green;
                    break;
                case 1:
                    color = CardColor.Red;
                    break;
                case 2:
                    color = CardColor.Yellow;
                    break;
            }
        }

        for (int i = 0; i < 2; i++)
        {
            cards.Add(new Card(CardColor.Black, CardType.Change_color));
            cards.Add(new Card(CardColor.Black, CardType.Draw_4));
            cards.Add(new Card(CardColor.Black, CardType.Switch_cards_all));
        }

        int count = cards.Count;
        int last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            int r = UnityEngine.Random.Range(i, count);
            Card tmp = cards[i];
            cards[i] = cards[r];
            cards[r] = tmp;
        }
    }

    private void OnMouseDown()
    {
        DrawCard(playerHand);
    }

    public void DrawCard(HandController handController)
    {
        //handController.AddCardToHand()
    }
}
