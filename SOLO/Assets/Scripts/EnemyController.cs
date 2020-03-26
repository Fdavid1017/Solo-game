using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CenterController centerController;
    public DrawPackController drawPackController;
    public GameManager gameManager;

    HandController handController;

    // Start is called before the first frame update
    void Start()
    {
        handController = GetComponent<HandController>();
    }

    public IEnumerator DoTurn()
    {
        List<GameObject> placeable = GetPlaceableCards();

        if (placeable.Count == 0 || UnityEngine.Random.Range(0, 100) > 90)
        {
            drawPackController.DrawCard(handController);
        }
        else
        {
            GameObject toPlace = placeable[UnityEngine.Random.Range(0, placeable.Count - 1)];
            Debug.Log("Placing: " + toPlace.GetComponent<Card>().color + " - " + toPlace.GetComponent<Card>().type);
        }

        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(0.5f, 3f));

        gameManager.DoNextTurn();
    }

    List<GameObject> GetPlaceableCards()
    {
        Card centerCard = centerController.TopCard;

        List<GameObject> hand = handController.Cards;
        List<GameObject> placeable = new List<GameObject>();

        foreach (GameObject item in hand)
        {
            Card card = item.GetComponent<Card>();
            if (centerCard == null || card.color == CardColor.Black || card.color == centerCard.color || card.type == centerCard.type)
            {
                placeable.Add(item);
            }
        }

        return placeable;
    }
}
