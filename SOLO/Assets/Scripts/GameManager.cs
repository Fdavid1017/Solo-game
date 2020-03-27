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
    [HideInInspector]
    public bool cardEffectUsed = true;

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

        if (Input.GetKeyDown(KeyCode.W))
        {
            for (int i = players.Count - 1; i > 0; i--)
            {
                HandController.ChangeHandCards(players[i].GetComponent<HandController>(), players[i - 1].GetComponent<HandController>(), cardBack);
            }
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
        if (!cardEffectUsed)
        {
            switch (centerController.TopCard.type)
            {
                case CardType.Skipp:
                    IncreaseNextPlayer();
                    Debug.Log(players[currentPlayer] + " skipped");
                    break;
                case CardType.Switch_direction:
                    roundDirection *= -1;
                    Debug.Log("Round direction changed");
                    break;
                case CardType.Change_cards:
                    Debug.Log("Changeing cards");
                    break;
                case CardType.Switch_cards_all:
                    if (roundDirection > 0)
                    {
                        for (int i = players.Count - 1; i > 0; i--)
                        {
                            HandController.ChangeHandCards(players[i].GetComponent<HandController>(), players[i - 1].GetComponent<HandController>(), cardBack);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < players.Count - 1; i++)
                        {
                            HandController.ChangeHandCards(players[i].GetComponent<HandController>(), players[i + 1].GetComponent<HandController>(), cardBack);
                        }
                    }

                    Debug.Log("Changeing all cards");
                    break;
                case CardType.Draw_2:
                    IncreaseNextPlayer();
                    drawPackController.DrawCard(players[currentPlayer].GetComponent<HandController>());
                    drawPackController.DrawCard(players[currentPlayer].GetComponent<HandController>());

                    Debug.Log("Drawing 2");
                    break;
                case CardType.Draw_4:
                    IncreaseNextPlayer();
                    drawPackController.DrawCard(players[currentPlayer].GetComponent<HandController>());
                    drawPackController.DrawCard(players[currentPlayer].GetComponent<HandController>());
                    drawPackController.DrawCard(players[currentPlayer].GetComponent<HandController>());
                    drawPackController.DrawCard(players[currentPlayer].GetComponent<HandController>());
                    Debug.Log("Drawing 4");
                    break;
            }
            cardEffectUsed = true;
        }

        IncreaseNextPlayer();
        if (players[currentPlayer].tag != "Player")
        {
            StartCoroutine(players[currentPlayer].GetComponent<EnemyController>().DoTurn());
        }
    }

    private void IncreaseNextPlayer()
    {
        if (roundDirection > 0)
        {
            currentPlayer = currentPlayer >= players.Count - 1 ? 0 : (currentPlayer + roundDirection);
        }
        else
        {
            currentPlayer = currentPlayer <= 0 ? players.Count - 1 : (currentPlayer + roundDirection);
        }
    }
}
