using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public CenterController centerController;
    public GameManager gameManager;
    public HandController playerHandController;
    public RotateAround highliteTrail;
    public List<GameObject> circles = new List<GameObject>();

    public void SetTopCardToRed() { centerController.ChangeTopCardColor(CardColor.Red); }
    public void SetTopCardToBlue() { centerController.ChangeTopCardColor(CardColor.Blue); }
    public void SetTopCardToGreen() { centerController.ChangeTopCardColor(CardColor.Green); }
    public void SetTopCardToYellow() { centerController.ChangeTopCardColor(CardColor.Yellow); }
    public void ChangeCardsWithPlayer(HandController hand) { gameManager.ChangeCards(playerHandController, hand); }
    public void ReorderCards() { playerHandController.ReorderCardsInList(); }
    public void ChangeTrail(int playerIndex) { highliteTrail.ChangeObjectToRotateAround(circles[gameManager.currentPlayer].transform); }
}
