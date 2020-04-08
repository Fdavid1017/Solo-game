using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MenuTextHighliteController : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Highlite()
    {
        anim.SetBool("highlite", true);
    }

    public void UnHighlite()
    {
        anim.SetBool("highlite", false);
    }
}
