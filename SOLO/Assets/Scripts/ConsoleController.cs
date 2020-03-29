using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleController : MonoBehaviour
{
    public static bool isDevConsoleActive = false;

    public GameObject ui;
    public GameManager gameManager;
    public CenterController centerController;
    public TextMeshProUGUI resultText;

    string[] inputCommand;

    enum Commands
    {
        help, clear, add_card
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ui.SetActive(true);
            isDevConsoleActive = true;
        }

        if (isDevConsoleActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ui.SetActive(false);
            isDevConsoleActive = false;
        }

        if (isDevConsoleActive && Input.GetKeyDown(KeyCode.Return))
        {
            resultText.text += "\n";
            Commands command = (Commands)System.Enum.Parse(typeof(Commands), inputCommand[0].Trim());
            switch (command)
            {
                case Commands.help:
                    var values = System.Enum.GetValues(typeof(Commands));
                    foreach (var item in values)
                    {
                        resultText.text += item.ToString() + "\n";
                    }
                    break;
                case Commands.clear:
                    resultText.text = "";
                    break;
                case Commands.add_card:
                    int toIndex = int.Parse(inputCommand[1]);
                    CardColor cardColor= (CardColor)System.Enum.Parse(typeof(CardColor), inputCommand[2].Trim());
                    CardType cardType= (CardType)System.Enum.Parse(typeof(CardType), inputCommand[3].Trim());
              /*      GameManager card=Instantiate(gameManager.drawPackController.red)
                    gameManager.players[toIndex].GetComponent<HandController>().AddCardToHand();*/
                    break;
            }
        }
    }

    public void OnInputTextChange(string text)
    {
        inputCommand = text.Trim().Split(' ');
    }
}
