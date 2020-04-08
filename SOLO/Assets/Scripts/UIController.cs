using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public CenterController centerController;
    public GameManager gameManager;
    public HandController playerHandController;
    public RotateAround highliteTrail;
    public List<GameObject> circles = new List<GameObject>();
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject lostMenu;

    bool isPaused = false;

    public void SetTopCardToRed() { centerController.ChangeTopCardColor(CardColor.Red); }
    public void SetTopCardToBlue() { centerController.ChangeTopCardColor(CardColor.Blue); }
    public void SetTopCardToGreen() { centerController.ChangeTopCardColor(CardColor.Green); }
    public void SetTopCardToYellow() { centerController.ChangeTopCardColor(CardColor.Yellow); }
    public void ChangeCardsWithPlayer(HandController hand) { gameManager.ChangeCards(playerHandController, hand); }
    public void ReorderCards() { playerHandController.ReorderCardsInList(); }
    public void ChangeTrail(int playerIndex) { highliteTrail.ChangeObjectToRotateAround(circles[gameManager.currentPlayer].transform); }
    public void UnPause()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowWinScreen() { winMenu.SetActive(true); }
    public void ShowLostScreen() { lostMenu.SetActive(true); }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                UnPause();
            }
        }
    }


}
