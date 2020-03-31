﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public CenterController centerController;
    public GameManager gameManager;
    public HandController playerHandController;

    public void SetTopCardToRed() { centerController.ChangeTopCardColor(CardColor.Red); }
    public void SetTopCardToBlue() { centerController.ChangeTopCardColor(CardColor.Blue); }
    public void SetTopCardToGreen() { centerController.ChangeTopCardColor(CardColor.Green); }
    public void SetTopCardToYellow() { centerController.ChangeTopCardColor(CardColor.Yellow); }
    public void ChangeCardsWithPlayer(HandController hand) { gameManager.ChangeCards(playerHandController, hand); }
}