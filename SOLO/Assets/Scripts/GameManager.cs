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
    public Sprite cardBack;
    public GameObject changeCardsUI;
    [HideInInspector]
    public sbyte roundDirection = 1;
    [HideInInspector]
    public int currentPlayer = -1;

    static bool isGameStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (!ConsoleController.isDevConsoleActive && !isGameStarted && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DealCards());
            currentPlayer = 0;
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
        centerController.SetTopCard(card.GetComponent<Card>(), null);
    }

    public void DoNextTurn()
    {
        IncreaseNextPlayer();
        if (players[currentPlayer].tag != "Player")
        {
            StartCoroutine(players[currentPlayer].GetComponent<EnemyController>().DoTurn());
        }
    }

    public void IncreaseNextPlayer()
    {
        currentPlayer = GetNextPlayerIndex();
        Debug.Log("Current player incresed to: " + currentPlayer);
    }

    public int GetNextPlayerIndex()
    {
        int tempCurrent = currentPlayer;
        if (roundDirection > 0)
        {
            tempCurrent = tempCurrent >= players.Count - 1 ? 0 : (tempCurrent + roundDirection);
        }
        else
        {
            tempCurrent = tempCurrent <= 0 ? players.Count - 1 : (tempCurrent + roundDirection);
        }
        return tempCurrent;
    }

    public void ChangeCards(HandController hand1, HandController hand2)
    {
        HandController.ChangeHandCards(hand1, hand2, cardBack);
        changeCardsUI.SetActive(false);
        DoNextTurn();
    }
}
