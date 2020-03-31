using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const byte DRAW_IF_SOLO_NOT_SAID = 2;

    public GameObject soloButton;
    public AvatarController avatarController;
    public DrawPackController drawPack;
    public GameManager gameManager;

    HandController handController;
    bool soloSaid = false;

    // Start is called before the first frame update
    void Start()
    {
        handController = GetComponent<HandController>();
    }

    public void ShowSoloButton(bool state)
    {
        soloButton.SetActive(state);
        soloSaid = false;
    }

    public void SaySolo()
    {
        Debug.Log(gameObject.name + " said solo");
        soloSaid = true;
        avatarController.SetSoloIcon(true);
        ShowSoloButton(false);
    }

    public void CheckSolo(bool drawIfNeeded = true,bool isCardRemoved=false)
    {
        //check if should say solo or said solo
        if (GameManager.isGameStarted)
        {
            if (handController.Cards.Count <= 1)
            {
                if (!soloSaid && drawIfNeeded)
                {
                    for (int i = 0; i < DRAW_IF_SOLO_NOT_SAID; i++)
                    {
                        drawPack.DrawCard(handController);
                    }
                }
            }
            else if (handController.Cards.Count == 2 && !isCardRemoved)
            {
                ShowSoloButton(true);
            }
            else
            {
                ShowSoloButton(false);
                avatarController.SetSoloIcon(false);
            }
        }
    }
}
