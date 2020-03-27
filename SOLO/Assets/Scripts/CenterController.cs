using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour
{
    private Card topCard = null;
    private HandController lastPlacer;

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

    public Card TopCard { get => topCard; }

    public bool SetTopCard(Card value, HandController placer)
    {
        if (topCard == null || topCard.color == CardColor.Black || value.color == CardColor.Black || value.color == topCard.color || value.type == topCard.type)
        {
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
            gameManager.cardEffectUsed = false;
            if (placer != null && (topCard.type == CardType.Change_color || topCard.type == CardType.Draw_4))
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
                    gameManager.DoNextTurn();
                }
            }
            else
            {
                gameManager.DoNextTurn();
            }

            return true;
        }
        return false;
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
