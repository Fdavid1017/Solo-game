using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DragController : MonoBehaviour
{
    const byte HIGHLIHTE_ORDER_LAYER = 20;

    public float snapSpeed = 1f;
    public HandController handController;
    public CenterController centerController;
    [HideInInspector]
    public bool isDragable = true;

    Vector3 offset;
    float mouseZCoordinate;
    Vector3 moveToPosition;
    int defaultLayer = 0;
    GameManager gameManager;
    DropShadow dropShadow;
    Vector3 shadowOffset = new Vector3(0f, 0f, 0.1f);
    Vector3 baseSize;
    Vector3 highliteSize = new Vector3(0.115f, 0.115f, 0.4f);
    bool isDraging = false;

    public Vector3 MoveToPosition { get => moveToPosition; set => moveToPosition = value; }

    private void OnMouseDown()
    {
        if (isDragable && gameManager.players[gameManager.currentPlayer].tag == "Player")
        {
            mouseZCoordinate = Camera.main.WorldToScreenPoint(transform.localPosition).z;
            offset = transform.localPosition - GetMouseWorldPos();
            shadowOffset = new Vector3(3f, -2.5f, 0.1f);
            GetComponent<Card>().scaleToShrink = highliteSize;
            GetComponent<SpriteRenderer>().sortingOrder = HIGHLIHTE_ORDER_LAYER + 1;
            isDraging = true;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (isDragable && gameManager.players[gameManager.currentPlayer].tag == "Player")
        {
            transform.localPosition = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        shadowOffset = new Vector3(0f, 0f, 0.1f);
        GetComponent<Card>().scaleToShrink = baseSize;
        GetComponent<SpriteRenderer>().sortingOrder = defaultLayer;
        isDraging = false;

        if (isDragable && gameManager.players[gameManager.currentPlayer].tag == "Player")
        {
            Vector3 mousePoint = GetMouseWorldPos();
            if ((mousePoint.x > centerController.gameObject.transform.position.x - 1.5 &&
                mousePoint.x < centerController.gameObject.transform.position.x + 1.5)
                && (mousePoint.y > centerController.gameObject.transform.position.y - 1.5 &&
                mousePoint.y < centerController.gameObject.transform.position.y + 1.5))
            {
                if (Card.SelectedCards.Count == 0)
                {
                    if (!centerController.SetTopCard(gameObject.GetComponent<Card>(), handController))
                    {
                        Card.ClearSelectedCards();
                    }
                }
                else
                {
                    for (int i = 0; i < Card.SelectedCards.Count - 1; i++)
                    {
                        centerController.SetTopCard(Card.SelectedCards[i], handController, true);
                    }
                    centerController.SetTopCard(Card.SelectedCards[Card.SelectedCards.Count - 1], handController);
                    Card.ClearSelectedCards();
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (isDragable)
        {
            GetComponent<SpriteRenderer>().sortingOrder = HIGHLIHTE_ORDER_LAYER;
        }
    }

    private void OnMouseExit()
    {
        if (isDragable && !isDraging)
        {
            GetComponent<SpriteRenderer>().sortingOrder = defaultLayer;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && isDragable && gameManager.players[gameManager.currentPlayer].tag == "Player")
        {
            Card thisCard = GetComponent<Card>();
            if (thisCard != null)
            {
                if (thisCard.isSelected)
                {
                    Card.RemoveCardFromSelected(thisCard);
                    thisCard.isSelected = false;
                }
                else
                {
                    if (centerController.CheckIfCardAcceptable(thisCard))
                    {
                        if (Card.SelectedCards.Count == 0 || Card.SelectedCards.TrueForAll(x => x.color == thisCard.color && x.type == thisCard.type))
                        {
                            Card.AddCardToSelected(thisCard);
                            thisCard.isSelected = true;
                        }
                    }
                }
            }
        }
    }

    private void Awake()
    {
        moveToPosition = transform.localPosition;
        defaultLayer = GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dropShadow = GetComponent<DropShadow>();
        dropShadow.offset = new Vector3(0f, 0f, 0.1f);
        baseSize = transform.localScale;
    }

    private void Update()
    {
        if (transform.localPosition != moveToPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, moveToPosition, snapSpeed * Time.deltaTime);
        }

        if (dropShadow.offset != shadowOffset)
        {
            dropShadow.offset = Vector3.Lerp(dropShadow.offset, shadowOffset, 0.5f);
        }
    }
}
