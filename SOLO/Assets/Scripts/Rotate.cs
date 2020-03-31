using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 30f;

    Vector3 scaleToShrink;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = transform.localScale;
        scaleToShrink = defaultScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);

        if (transform.localScale != scaleToShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleToShrink, 0.5f);
        }
        else
        {
            if (speed < 0)
            {
                scaleToShrink = defaultScale;
            }
            else
            {
                Vector3 t = defaultScale;
                t.x *= -1;
                scaleToShrink = t;
            }
        }
    }

    public void ChangeDirection()
    {
        speed *= -1;
        scaleToShrink = new Vector3(0, 0, 0);
    }
}
