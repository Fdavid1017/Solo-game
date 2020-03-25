using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public float snapSpeed = 1f;
    public HandController handController;
    public CenterController centerController;

    Vector3 offset;
    float mouseZCoordinate;
    Vector3 moveToPosition;
    bool isDragable = true;

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
        if ((mousePoint.x > -1.5 && mousePoint.x < 1.5) && (mousePoint.y > -1.5 && mousePoint.y < 1.5))
        {
            handController.RemoveCard(this.gameObject);
            CenterController centerController = GameObject.FindObjectOfType<CenterController>();
            moveToPosition = centerController.gameObject.transform.position;
            centerController.topCard = gameObject.GetComponent<Card>();

            transform.parent = centerController.gameObject.transform;

            isDragable = false;
        }
    }

    private void Awake()
    {
        moveToPosition = transform.localPosition;
    }

    private void Update()
    {
        if (transform.localPosition != moveToPosition)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, moveToPosition, snapSpeed * Time.deltaTime);
        }
    }
}
