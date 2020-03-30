using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DropShadow : MonoBehaviour
{
    public Vector3 offset;
    public Material material;

    GameObject shadow;
    SpriteRenderer parentRenderer;
    SpriteRenderer renderer;

    private void Start()
    {
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;
        shadow.transform.localScale = new Vector3(1, 1, 1);

        shadow.transform.localPosition = offset;
        shadow.transform.localRotation = Quaternion.identity;

        parentRenderer = GetComponent<SpriteRenderer>();
        renderer = shadow.AddComponent<SpriteRenderer>();
        renderer.sprite = parentRenderer.sprite;
        renderer.material = material;

        renderer.sortingLayerName = parentRenderer.sortingLayerName;
        renderer.sortingOrder = parentRenderer.sortingOrder;
    }

    private void LateUpdate()
    {
        shadow.transform.localPosition = offset;
        renderer.sortingLayerName = parentRenderer.sortingLayerName;
        renderer.sortingOrder = parentRenderer.sortingOrder;
    }
}
