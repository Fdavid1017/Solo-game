using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Vector2 handSizeBoundaries = new Vector2(-3, 3);
    public GameObject cardToTest;

    List<GameObject> cards;

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject newCard = Instantiate(cardToTest, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), transform);
            newCard.GetComponent<DragController>().handController = this;
            AddCardToHand(newCard);
        }
    }

    public void AddCardToHand(GameObject card)
    {
        cards.Add(card);
        ReorderCards();
    }

    private void ReorderCards()
    {
        if (cards.Count > 1)
        {
            float spacing = Mathf.Abs(handSizeBoundaries.x - handSizeBoundaries.y) / (cards.Count - 1);
            for (int i = 0; i < cards.Count; i++)
            {
                Vector3 newPosition = new Vector3(0, 0, 0);
                newPosition.x = handSizeBoundaries.x + (spacing * i);
                cards[i].GetComponent<DragController>().MoveToPosition = newPosition;
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
    }
}
