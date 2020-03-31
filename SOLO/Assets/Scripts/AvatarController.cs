using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvatarController : MonoBehaviour
{
    public List<Sprite> avatarPictures;
    public SVGImage avatarContainer;
    public SVGImage circleContainer;
    public int playerNumber = 0;
    public GameManager gameManager;
    public TextMeshProUGUI cardNumberText;
    public GameObject soloLogo;

    static List<byte> usedIndexes = new List<byte>();

    // Start is called before the first frame update
    void Start()
    {
        byte avatarIndex = 0;
        do
        {
            avatarIndex = (byte)UnityEngine.Random.Range(0, avatarPictures.Count);
        } while (usedIndexes.Contains(avatarIndex));
        usedIndexes.Add(avatarIndex);
        avatarContainer.sprite = avatarPictures[avatarIndex];
    }

    // Update is called once per frame
    void Update()
    {
        cardNumberText.SetText(gameManager.players[playerNumber].GetComponent<HandController>().Cards.Count.ToString());

        if (gameManager.currentPlayer == playerNumber)
        {
            circleContainer.color = new Color32(0, 229, 0, 255);
        }
        else
        {
            circleContainer.color = new Color32(255, 255, 255, 255);
        }
    }

    public void SetSoloIcon(bool state)
    {
        soloLogo.SetActive(state);
    }
}
