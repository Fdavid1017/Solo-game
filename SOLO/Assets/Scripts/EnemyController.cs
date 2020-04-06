using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CenterController centerController;
    public DrawPackController drawPackController;
    public GameManager gameManager;
    public AvatarController avatarController;

    HandController handController;

    // Start is called before the first frame update
    void Start()
    {
        handController = GetComponent<HandController>();
    }

    public IEnumerator DoTurn()
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(3f, 5f));

        List<GameObject> placeable = GetPlaceableCards();
        if (placeable.Count == 0 || UnityEngine.Random.Range(0, 100) > 90)
        {
            int drawCount = centerController.drawCount == 0 ? 1 : centerController.drawCount;
            for (int i = 0; i < drawCount; i++)
            {
                drawPackController.DrawCard(handController);
            }
            if (centerController.drawCount != 0)
            {
                centerController.cardEffectUsed = true;
                centerController.drawCount = 0;
            }
            gameManager.DoNextTurn();
        }
        else
        {
            GameObject toPlace = placeable[UnityEngine.Random.Range(0, placeable.Count - 1)];
            handController.RemoveCard(toPlace);
            centerController.SetTopCard(toPlace.GetComponent<Card>(), handController);
        }

        if (handController.Cards.Count == 1)
        {
            avatarController.SetSoloIcon(true);
        }
        else
        {
            avatarController.SetSoloIcon(false);
        }
    }

    List<GameObject> GetPlaceableCards()
    {
        Card centerCard = centerController.TopCard;

        List<GameObject> hand = handController.Cards;
        List<GameObject> placeable = new List<GameObject>();

        if ((centerCard == null || centerCard.color == CardColor.Black) && centerCard.type != CardType.Draw_4)
        {
            return hand;
        }

        foreach (GameObject item in hand)
        {
            Card card = item.GetComponent<Card>();
            if (card.color == CardColor.Black || card.color == centerCard.color || card.type == centerCard.type)
            {
                if (centerCard == null || centerController.cardEffectUsed || (centerCard.type != CardType.Draw_2 && centerCard.type != CardType.Draw_4))
                {
                    placeable.Add(item);
                }
                else
                {
                    if (centerCard.type == CardType.Draw_2 && (card.type == CardType.Draw_2 || card.type == CardType.Draw_4))
                    {
                        placeable.Add(item);
                    }
                    if (centerCard.type == CardType.Draw_4 && card.type == CardType.Draw_4)
                    {
                        placeable.Add(item);
                    }
                }
            }
        }

        return placeable;
    }
}
