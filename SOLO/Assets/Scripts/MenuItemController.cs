using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemController : MonoBehaviour
{
    public Vector3 highliteMultiplier = new Vector3(1.2f, 1.2f, 1.2f);
    public Vector3 hideMultiplier = new Vector3(0.8f, 0.8f, 0.8f);

    Vector3 scaleToShrink;
    Vector3 defaultScale;
    bool isHideing = false;

    private void Start()
    {
        scaleToShrink = defaultScale = transform.localScale;
    }

    private void Update()
    {
        if (transform.localScale != scaleToShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scaleToShrink, 0.5f);
        }
    }

    public void HighliteItem()
    {
        if (!isHideing)
        {
            scaleToShrink = highliteMultiplier;
        }
    }

    public void ShrinkItem()
    {
        if (!isHideing)
        {
            scaleToShrink = hideMultiplier;
        }
    }

    public void ResetItem()
    {
        if (!isHideing)
        {
            scaleToShrink = defaultScale;
        }
    }

    public void HideItem()
    {
        isHideing = true;
        scaleToShrink = new Vector3(0, 0, 0);
        Invoke("SetHidingFalse", 0.6f);
    }

    void SetHidingFalse() { isHideing = false; }
}
