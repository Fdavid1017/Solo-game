using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Vector3 sizeToShrink;
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
            sizeToShrink = highliteSize;
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
        sizeToShrink = baseSize;
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
                CenterController centerController = GameObject.FindObjectOfType<CenterController>();
                if (!centerController.SetTopCard(gameObject.GetComponent<Card>(), handController))
                {
                    return;
                }

                handController.RemoveCard(this.gameObject);
                moveToPosition = centerController.gameObject.transform.position;
                transform.parent = centerController.gameObject.transform;
                isDragable = false;
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
        sizeToShrink = baseSize;
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

        if (transform.localScale != sizeToShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, sizeToShrink, 0.5f);
        }
    }
}
