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
            value.RevealCard();
            value.transform.parent = transform;
            Vector3 t;
            if (topCard != null)
            {
                t = topCard.GetComponent<DragController>().MoveToPosition;
                Debug.Log("Def:" + t);
                t.z = 0;
                Debug.Log("Edited:" + t);
                topCard.transform.position = t;
                topCard.GetComponent<DragController>().MoveToPosition = t;
                Destroy(topCard.gameObject, 2);
            }

            topCard = value;
            t = transform.position;
            t.z = 1f;
            topCard.GetComponent<DragController>().MoveToPosition = t;
            topCard.transform.rotation = new Quaternion(0, 0, 0, 0);
            Debug.Log(topCard.GetComponent<DragController>().MoveToPosition);
            gameManager.DoNextTurn();

            return true;
        }
        return false;
    }
}
