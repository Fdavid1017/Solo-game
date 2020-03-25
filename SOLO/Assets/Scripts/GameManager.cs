using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const byte STARTING_CARD_COUNT = 8;

    public GameObject[] players;
    public DrawPackController drawPackController;
    public CenterController centerController;
    public float dealingSpeed = 0.2f;

    sbyte roundDirection = 1;
    sbyte currentPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DealCards());
        }
    }

    IEnumerator DealCards()
    {
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < STARTING_CARD_COUNT; j++)
            {
                HandController current = players[i].GetComponent<HandController>();
                drawPackController.DrawCard(current);
                yield return new WaitForSecondsRealtime(dealingSpeed);
            }
        }

        GameObject card = drawPackController.GetTopCard();
        card.GetComponent<DragController>().centerController = centerController;
        card.GetComponent<DragController>().MoveToPosition = centerController.gameObject.transform.position;
        card.gameObject.transform.parent = centerController.transform;
        centerController.SetTopCard(card.GetComponent<Card>());
    }
}
