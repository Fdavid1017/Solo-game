using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const byte STARTING_CARD_COUNT = 8;

    public List<GameObject> players;
    public DrawPackController drawPackController;
    public CenterController centerController;
    public float dealingSpeed = 0.2f;

    sbyte roundDirection = 1;
    int currentPlayer = -1;

    static bool isGameStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DealCards());
            isGameStarted = true;
        }
    }

    IEnumerator DealCards()
    {
        for (int i = 0; i < players.Count; i++)
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

    public void DoNextTurn()
    {
        Debug.Log("Do next turn started");
        if (roundDirection > 0)
        {
            currentPlayer = currentPlayer >= players.Count - 1 ? 0 : (currentPlayer + roundDirection);
        }
        else
        {
            currentPlayer = currentPlayer <= 0 ? players.Count - 1 : (currentPlayer + roundDirection);
        }

        if (players[currentPlayer].tag != "Player")
        {
            StartCoroutine(players[currentPlayer].GetComponent<EnemyController>().DoTurn());
        }
    }
}
