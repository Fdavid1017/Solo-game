using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float speed;
    public Transform target;

    bool doRotate = false;
    bool doMove = true;
    Vector3 moveToPosition = new Vector3(0, 0, 0);
    Vector3 zAxis = new Vector3(0, 0, 0.1f);

    private void Start()
    {
        moveToPosition = target.transform.position;
        moveToPosition.x -= 0.85f;
    }

    void FixedUpdate()
    {
        if (doRotate)
        {
            transform.RotateAround(target.position, zAxis, speed);
        }

        if (doMove)
        {
            if (transform.position != moveToPosition)
            {
                transform.position = Vector3.Lerp(transform.position, moveToPosition, 0.5f);
            }
            else
            {
                doMove = false;
                doRotate = true;
            }
        }
    }

    public void ChangeObjectToRotateAround(Transform position)
    {
        doMove = true;
        doRotate = false;
        moveToPosition = position.position;
        moveToPosition.x -= 0.85f;
        target = position;
    }
}
