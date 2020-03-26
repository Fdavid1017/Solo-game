using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour
{
    private Card topCard = null;

    public GameManager gameManager;

    public Card TopCard { get => topCard; }

    public bool SetTopCard(Card value)
    {
        if (topCard == null || value.color == CardColor.Black || value.color == topCard.color || value.type == topCard.type)
        {
            Vector3 t;
            if (topCard != null)
            {
                t = topCard.gameObject.transform.position;
                t.z = 0;
                topCard.gameObject.transform.position = t;
                Destroy(topCard.gameObject, 2);
            }

            topCard = value;
            t = topCard.GetComponent<DragController>().MoveToPosition;
            t.z = 0.1f;
            topCard.GetComponent<DragController>().MoveToPosition = t;

            gameManager.DoNextTurn();

            return true;
        }
        return false;
    }
}
