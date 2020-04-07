using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandController : MonoBehaviour
{
    public Vector2 handSizeBoundaries = new Vector2(-3, 3);

    List<GameObject> cards;

    public List<GameObject> Cards { get => cards; }

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<GameObject>();
    }

    public void AddCardToHand(GameObject card)
    {
        card.transform.parent = transform;
        cards.Add(card);
        ReorderCards();
        CheckSolo(false);
        Debug.Log(cards.Count);
        foreach (var item in cards)
        {
            Debug.Log(item.GetComponent<Card>().color + " - " + item.GetComponent<Card>().type);
        }
        Debug.Log("----------------------------------");
    }

    private void ReorderCards()
    {
        if (cards.Count > 1)
        {
            Vector3 handBoundairesTemp = handSizeBoundaries;
            if (cards.Count == 2)
            {
                float distance = (2.3f / cards.Count + 0.15f) * cards.Count;

                handBoundairesTemp.x = 0 - distance / cards.Count;
                handBoundairesTemp.y = 0 + distance / cards.Count;
            }

            float spacing = Mathf.Abs(handBoundairesTemp.x - handBoundairesTemp.y) / (cards.Count - 1);
            for (int i = 0; i < cards.Count; i++)
            {
                Vector3 newPosition = new Vector3(0, 0, 0);
                newPosition.x = handBoundairesTemp.x + (spacing * i);
                newPosition.z = 0.001f * -i;
                cards[i].GetComponent<DragController>().MoveToPosition = newPosition;
                cards[i].transform.localRotation = new Quaternion(0, 0, 0, 0);
            }
        }
        else if (cards.Count > 0)
        {
            cards[0].GetComponent<DragController>().MoveToPosition = new Vector3(0, 0, 0);
        }
    }

    public void RemoveCard(GameObject card)
    {
        cards.Remove(card);
        ReorderCards();
        CheckSolo(true);
        Debug.Log(cards.Count);
        foreach (var item in cards)
        {
            Debug.Log(item.GetComponent<Card>().color + " - " + item.GetComponent<Card>().type);
        }
        Debug.Log("----------------------------------");
    }

    public void ReorderCardsInList()
    {
        cards = cards.OrderBy(card => card.GetComponent<Card>().color).ThenBy(card => card.GetComponent<Card>().type).ToList();
        ReorderCards();
    }

    void CheckSolo(bool remove)
    {
        if (gameObject.tag == "Player")
        {
            GetComponent<PlayerController>().CheckSolo(false, remove);
        }
    }

    public static void ChangeHandCards(HandController hand1, HandController hand2, Sprite cardBack)
    {
        List<GameObject> temp = hand1.cards;
        hand1.cards = hand2.cards;
        hand2.cards = temp;

        foreach (GameObject card in hand1.cards)
        {
            card.transform.parent = hand1.gameObject.transform;
            card.GetComponent<DragController>().handController = hand1;
            if (hand1.tag == "Player")
            {
                card.GetComponent<Card>().RevealCard();
                card.GetComponent<DragController>().isDragable = true;
            }
            else
            {
                card.GetComponent<SpriteRenderer>().sprite = cardBack;
                card.GetComponent<DragController>().isDragable = false;
            }

        }
        foreach (GameObject card in hand2.cards)
        {
            card.transform.parent = hand2.gameObject.transform;
            card.GetComponent<DragController>().handController = hand2;
            if (hand2.tag == "Player")
            {
                card.GetComponent<Card>().RevealCard();
                card.GetComponent<DragController>().isDragable = true;
            }
            else
            {
                card.GetComponent<SpriteRenderer>().sprite = cardBack;
                card.GetComponent<DragController>().isDragable = false;
            }

        }

        hand1.ReorderCards();
        hand2.ReorderCards();
    }
}
