using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour
{
    private Card topCard = null;
    private HandController lastPlacer;
    bool cardEffectUsed = true;
    int drawCount = 0;

    public GameManager gameManager;
    public Sprite draw_4_red;
    public Sprite draw_4_green;
    public Sprite draw_4_blue;
    public Sprite draw_4_yellow;
    public Sprite change_color_red;
    public Sprite change_color_green;
    public Sprite change_color_blue;
    public Sprite change_color_yellow;
    public GameObject colorChangerPanel;
    public GameObject changeCardsUI;
    public DrawPackController drawPackController;
    public GameObject directionArrows;

    public Card TopCard { get => topCard; }

    public bool SetTopCard(Card value, HandController placer)
    {
        if (topCard == null || topCard.color == CardColor.Black || value.color == CardColor.Black || value.color == topCard.color || value.type == topCard.type)
        {
            if (topCard != null && !cardEffectUsed)
            {
                if ((topCard.type == CardType.Draw_2 && (value.type != CardType.Draw_2 && value.type != CardType.Draw_4)))
                {
                    return false;
                }
                if ((topCard.type == CardType.Draw_4 && value.type != CardType.Draw_4))
                {
                    return false;
                }
            }

            lastPlacer = placer;
            value.RevealCard();
            value.transform.parent = transform;
            Vector3 t;
            if (topCard != null)
            {
                topCard.GetComponent<SpriteRenderer>().sortingOrder = 1;
                Destroy(topCard.gameObject, 2);
            }

            topCard = value;
            t = transform.position;
            topCard.GetComponent<SpriteRenderer>().sortingOrder = 2;
            topCard.GetComponent<DragController>().MoveToPosition = t;
            topCard.transform.rotation = new Quaternion(0, 0, 0, 0);
            cardEffectUsed = false;
            if (placer != null)
            {
                if (placer.Cards.Count == 0)
                {
                    Debug.Log(placer.name + " won the game");
                    return true;
                }

                if (!cardEffectUsed)
                {
                    switch (topCard.type)
                    {
                        case CardType.Skipp:
                            gameManager.IncreaseNextPlayer();
                            Debug.Log(gameManager.players[gameManager.currentPlayer] + " skipped");
                            gameManager.DoNextTurn();
                            break;
                        case CardType.Switch_direction:
                            gameManager.roundDirection *= -1;
                            Vector3 scale = directionArrows.transform.localScale;
                            scale.x *= -1;
                            directionArrows.transform.localScale = scale;
                            Debug.Log("Round direction changed");
                            gameManager.DoNextTurn();
                            break;
                        case CardType.Change_cards:
                            if (placer.tag == "Player")
                            {
                                changeCardsUI.SetActive(true);
                            }
                            else
                            {
                                byte playerIndex = (byte)UnityEngine.Random.Range(0, 2);
                                switch (playerIndex)
                                {
                                    case 0:
                                        gameManager.ChangeCards(placer, gameManager.players[playerIndex].GetComponent<HandController>());
                                        break;
                                    case 1:
                                        gameManager.ChangeCards(placer, gameManager.players[playerIndex].GetComponent<HandController>());
                                        break;
                                    case 2:
                                        gameManager.ChangeCards(placer, gameManager.players[playerIndex].GetComponent<HandController>());
                                        break;
                                }
                                gameManager.DoNextTurn();
                            }
                            Debug.Log("Changeing cards");
                            break;
                        case CardType.Switch_cards_all:
                            if (gameManager.roundDirection > 0)
                            {
                                for (int i = gameManager.players.Count - 1; i > 0; i--)
                                {
                                    HandController.ChangeHandCards(gameManager.players[i].GetComponent<HandController>(), gameManager.players[i - 1].GetComponent<HandController>(), gameManager.cardBack);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < gameManager.players.Count - 1; i++)
                                {
                                    HandController.ChangeHandCards(gameManager.players[i].GetComponent<HandController>(), gameManager.players[i + 1].GetComponent<HandController>(), gameManager.cardBack);
                                }
                            }

                            Debug.Log("Changeing all cards");
                            gameManager.DoNextTurn();
                            break;

                        case CardType.Draw_2:
                            drawCount += 2;
                            bool canPlace = false;
                            for (int i = 0; i < gameManager.players[gameManager.GetNextPlayerIndex()].GetComponent<HandController>().Cards.Count && !canPlace; i++)
                            {
                                if (gameManager.players[gameManager.GetNextPlayerIndex()].GetComponent<HandController>().Cards[i].GetComponent<Card>().type == CardType.Draw_2 ||
                                    gameManager.players[gameManager.GetNextPlayerIndex()].GetComponent<HandController>().Cards[i].GetComponent<Card>().type == CardType.Draw_4)
                                {
                                    canPlace = true;
                                }
                            }

                            if (!canPlace)
                            {
                                gameManager.IncreaseNextPlayer();
                                for (int i = 0; i < drawCount; i++)
                                {
                                    drawPackController.DrawCard(gameManager.players[gameManager.currentPlayer].GetComponent<HandController>());
                                }
                                drawCount = 0;
                            }
                            Debug.Log("Drawing 2");
                            gameManager.DoNextTurn();
                            break;
                        case CardType.Draw_4:
                            drawCount += 4;
                            bool canPlace2 = false;
                            for (int i = 0; i < gameManager.players[gameManager.GetNextPlayerIndex()].GetComponent<HandController>().Cards.Count && !canPlace2; i++)
                            {
                                if (gameManager.players[gameManager.GetNextPlayerIndex()].GetComponent<HandController>().Cards[i].GetComponent<Card>().type == CardType.Draw_4)
                                {
                                    canPlace2 = true;
                                }
                            }

                            if (!canPlace2)
                            {
                                gameManager.IncreaseNextPlayer();
                                for (int i = 0; i < drawCount; i++)
                                {
                                    drawPackController.DrawCard(gameManager.players[gameManager.currentPlayer].GetComponent<HandController>());
                                }
                                drawCount = 0;
                            }

                            ChangeTopColor(placer);
                            Debug.Log("Drawing 4");
                            break;
                        case CardType.Change_color:
                            ChangeTopColor(placer);
                            break;
                        default:
                            gameManager.DoNextTurn();
                            break;
                    }
                    cardEffectUsed = true;
                }
            }
            return true;
        }
        return false;
    }

    private void ChangeTopColor(HandController placer)
    {
        if (placer.tag == "Player")
        {
            colorChangerPanel.SetActive(true);
        }
        else
        {
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    ChangeTopCardColor(CardColor.Blue);
                    break;
                case 1:
                    ChangeTopCardColor(CardColor.Green);
                    break;
                case 2:
                    ChangeTopCardColor(CardColor.Red);
                    break;
                case 3:
                    ChangeTopCardColor(CardColor.Yellow);
                    break;
            }
        }
    }

    public bool ChangeTopCardColor(CardColor color)
    {
        if (topCard.type == CardType.Change_color || topCard.type == CardType.Draw_4)
        {
            topCard.color = color;
            switch (color)
            {
                case CardColor.Red:
                    switch (topCard.type)
                    {
                        case CardType.Change_color:
                            topCard.GetComponent<SpriteRenderer>().sprite = change_color_red;
                            break;
                        case CardType.Draw_4:
                            topCard.GetComponent<SpriteRenderer>().sprite = draw_4_red;
                            break;
                    }
                    break;
                case CardColor.Green:
                    switch (topCard.type)
                    {
                        case CardType.Change_color:
                            topCard.GetComponent<SpriteRenderer>().sprite = change_color_green;
                            break;
                        case CardType.Draw_4:
                            topCard.GetComponent<SpriteRenderer>().sprite = draw_4_green;
                            break;
                    }
                    break;
                case CardColor.Yellow:
                    switch (topCard.type)
                    {
                        case CardType.Change_color:
                            topCard.GetComponent<SpriteRenderer>().sprite = change_color_yellow;
                            break;
                        case CardType.Draw_4:
                            topCard.GetComponent<SpriteRenderer>().sprite = draw_4_yellow;
                            break;
                    }
                    break;
                case CardColor.Blue:
                    switch (topCard.type)
                    {
                        case CardType.Change_color:
                            topCard.GetComponent<SpriteRenderer>().sprite = change_color_blue;
                            break;
                        case CardType.Draw_4:
                            topCard.GetComponent<SpriteRenderer>().sprite = draw_4_blue;
                            break;
                    }
                    break;
            }
            colorChangerPanel.SetActive(false);
            gameManager.DoNextTurn();
            return true;
        }
        return false;
    }
}
