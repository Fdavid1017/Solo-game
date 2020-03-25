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

    public Vector3 MoveToPosition { get => moveToPosition; set => moveToPosition = value; }

    private void OnMouseDown()
    {
        mouseZCoordinate = Camera.main.WorldToScreenPoint(transform.localPosition).z;
        offset = transform.localPosition - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (isDragable)
        {
            transform.localPosition = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        Vector3 mousePoint = GetMouseWorldPos();
        if ((mousePoint.x > centerController.gameObject.transform.position.x - 1.5 &&
            mousePoint.x < centerController.gameObject.transform.position.x + 1.5)
            && (mousePoint.y > centerController.gameObject.transform.position.y - 1.5 &&
            mousePoint.y < centerController.gameObject.transform.position.y + 1.5))
        {
            CenterController centerController = GameObject.FindObjectOfType<CenterController>();
            if (!centerController.SetTopCard(gameObject.GetComponent<Card>()))
            {
                return;
            }

            handController.RemoveCard(this.gameObject);
            moveToPosition = centerController.gameObject.transform.position;
            transform.parent = centerController.gameObject.transform;
            isDragable = false;
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
        if (isDragable)
        {
            GetComponent<SpriteRenderer>().sortingOrder = defaultLayer;
        }
    }

    private void Awake()
    {
        moveToPosition = transform.localPosition;
        defaultLayer = GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Update()
    {
        if (transform.localPosition != moveToPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, moveToPosition, snapSpeed * Time.deltaTime);
        }
    }
}
