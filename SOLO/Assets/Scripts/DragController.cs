using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public float snapSpeed = 1f;
    public float randomRotationAmmount = 12f;

    Vector3 offset;
    float mouseZCoordinate;
    Vector3 moveToPosition;

    public Vector3 MoveToPosition { get => moveToPosition; set => moveToPosition = value; }

    private void OnMouseDown()
    {
        mouseZCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    private void OnMouseUp()
    {
        Vector3 mousePoint = GetMouseWorldPos();
        if ((mousePoint.x > -1.5 && mousePoint.x < 1.5) && (mousePoint.y > -1.5 && mousePoint.y < 1.5))
        {
            moveToPosition = new Vector3(0, 0, 0);
        }
        else
        {
            moveToPosition = transform.position;
        }
    }

    private void Awake()
    {
        moveToPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position != moveToPosition)
        {
            transform.position = Vector3.Lerp(transform.position, moveToPosition, snapSpeed * Time.deltaTime);
        }
    }
}
