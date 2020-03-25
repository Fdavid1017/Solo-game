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
    public List<GameObject> blackCardsPrefabs;
    public CenterController centerController;

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
        GameObject card = GetTopCard();
        card.transform.parent = handController.transform;
        card.GetComponent<DragController>().handController = handController;
        card.GetComponent<DragController>().centerController = centerController;
        handController.AddCardToHand(card);
    }

    public GameObject GetTopCard()
    {
        Card topCard = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        GameObject card;
        switch (topCard.color)
        {
            case CardColor.Red:
                switch (topCard.type)
                {
                    case CardType.One:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.One));
                        break;
                    case CardType.Two:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Two));
                        break;
                    case CardType.Three:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Three));
                        break;
                    case CardType.Four:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Four));
                        break;
                    case CardType.Five:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Five));
                        break;
                    case CardType.Six:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Six));
                        break;
                    case CardType.Seven:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Seven));
                        break;
                    case CardType.Eight:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Eight));
                        break;
                    case CardType.Nine:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Nine));
                        break;
                    case CardType.Skipp:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Skipp));
                        break;
                    case CardType.Switch_direction:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Switch_direction));
                        break;
                    case CardType.Change_cards:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Change_cards));
                        break;
                    case CardType.Draw_2:
                        card = Instantiate(redCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Draw_2));
                        break;
                    default:
                        card = new GameObject();
                        break;
                }
                break;
            case CardColor.Green:
                switch (topCard.type)
                {
                    case CardType.One:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.One));
                        break;
                    case CardType.Two:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Two));
                        break;
                    case CardType.Three:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Three));
                        break;
                    case CardType.Four:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Four));
                        break;
                    case CardType.Five:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Five));
                        break;
                    case CardType.Six:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Six));
                        break;
                    case CardType.Seven:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Seven));
                        break;
                    case CardType.Eight:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Eight));
                        break;
                    case CardType.Nine:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Nine));
                        break;
                    case CardType.Skipp:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Skipp));
                        break;
                    case CardType.Switch_direction:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Switch_direction));
                        break;
                    case CardType.Change_cards:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Change_cards));
                        break;
                    case CardType.Draw_2:
                        card = Instantiate(greenCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Draw_2));
                        break;
                    default:
                        card = new GameObject();
                        break;
                }
                break;
            case CardColor.Yellow:
                switch (topCard.type)
                {
                    case CardType.One:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.One));
                        break;
                    case CardType.Two:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Two));
                        break;
                    case CardType.Three:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Three));
                        break;
                    case CardType.Four:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Four));
                        break;
                    case CardType.Five:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Five));
                        break;
                    case CardType.Six:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Six));
                        break;
                    case CardType.Seven:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Seven));
                        break;
                    case CardType.Eight:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Eight));
                        break;
                    case CardType.Nine:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Nine));
                        break;
                    case CardType.Skipp:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Skipp));
                        break;
                    case CardType.Switch_direction:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Switch_direction));
                        break;
                    case CardType.Change_cards:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Change_cards));
                        break;
                    case CardType.Draw_2:
                        card = Instantiate(yellowCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Draw_2));
                        break;
                    default:
                        card = new GameObject();
                        break;
                }
                break;
            case CardColor.Blue:
                switch (topCard.type)
                {
                    case CardType.One:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.One));
                        break;
                    case CardType.Two:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Two));
                        break;
                    case CardType.Three:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Three));
                        break;
                    case CardType.Four:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Four));
                        break;
                    case CardType.Five:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Five));
                        break;
                    case CardType.Six:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Six));
                        break;
                    case CardType.Seven:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Seven));
                        break;
                    case CardType.Eight:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Eight));
                        break;
                    case CardType.Nine:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Nine));
                        break;
                    case CardType.Skipp:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Skipp));
                        break;
                    case CardType.Switch_direction:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Switch_direction));
                        break;
                    case CardType.Change_cards:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Change_cards));
                        break;
                    case CardType.Draw_2:
                        card = Instantiate(blueCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Draw_2));
                        break;
                    default:
                        card = new GameObject();
                        break;
                }
                break;
            case CardColor.Black:
                switch (topCard.type)
                {
                    case CardType.Switch_cards_all:
                        card = Instantiate(blackCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Switch_cards_all));
                        break;
                    case CardType.Change_color:
                        card = Instantiate(blackCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Change_color));
                        break;
                    case CardType.Draw_4:
                        card = Instantiate(blackCardsPrefabs.Find(x => x.GetComponent<Card>().type == CardType.Draw_4));
                        break;
                    default:
                        card = new GameObject();
                        break;
                }
                break;
            default:
                card = new GameObject();
                break;
        }
        card.transform.position = transform.position;
        return card;
    }
}
