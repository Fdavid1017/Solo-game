using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(2f, 5f));

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
            GameObject toPlace;
            if (PlayerPrefs.GetInt("difficulity", 0) == 0)
            {
                toPlace = placeable[UnityEngine.Random.Range(0, placeable.Count - 1)];
            }
            else
            {
                toPlace = null;
                Card topCard = centerController.TopCard;

                List<GameObject> changeCardCards = placeable.Where(x => x.GetComponent<Card>().type == CardType.Change_cards ||
                  x.GetComponent<Card>().type == CardType.Switch_cards_all).ToList();

                List<GameObject> cardsIfNextPlayerHesLowCards = placeable.Where(x => x.GetComponent<Card>().type == CardType.Draw_2 ||
                    x.GetComponent<Card>().type == CardType.Draw_4 || x.GetComponent<Card>().type == CardType.Switch_direction).ToList();

                List<GameObject> changeColorCards = placeable.Where(x => x.GetComponent<Card>().type == CardType.Change_color).ToList();

                int index = gameManager.GetPlayerIndexWithTheLeastAmmountOfCard(gameObject);
                HandController playerWithTheLeastCard = gameManager.players[index].GetComponent<HandController>();

                if (changeCardCards.Count > 0)
                {
                    //Has change cards
                    if (changeCardCards.Count >= 2)
                    {
                        toPlace = changeCardCards[UnityEngine.Random.Range(0, changeCardCards.Count - 1)];
                    }
                    else if (changeCardCards.Count == placeable.Count || playerWithTheLeastCard.Cards.Count < handController.Cards.Count - 1)
                    {
                        toPlace = changeCardCards[UnityEngine.Random.Range(0, changeCardCards.Count - 1)];
                    }
                }
                else if (cardsIfNextPlayerHesLowCards.Count > 0)
                {
                    //if the player next to him in the turn direction has less than 3 card use draw 2/4, change direction or skipp player card if has one
                    if (gameManager.players[gameManager.GetNextPlayerIndex()].GetComponent<HandController>().Cards.Count < 3)
                    {
                        toPlace = cardsIfNextPlayerHesLowCards[UnityEngine.Random.Range(0, cardsIfNextPlayerHesLowCards.Count - 1)];
                    }
                }
                else if (changeColorCards.Count > 0)
                {
                    //Has change color cards
                    List<GameObject> coloredToPlace = placeable.Where(x => x.GetComponent<Card>().color == centerController.TopCard.color).ToList();

                    if (coloredToPlace.Count > 0)
                    {
                        toPlace = coloredToPlace[UnityEngine.Random.Range(0, coloredToPlace.Count - 1)];
                    }
                }

                if (toPlace == null)
                {
                    toPlace = placeable[UnityEngine.Random.Range(0, placeable.Count - 1)];
                }
            }

            //Placing card
            handController.RemoveCard(toPlace);
            centerController.SetTopCard(toPlace.GetComponent<Card>(), handController);
        }


        //Setting the solo icon
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
