using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinButton : MonoBehaviour
{
    public Animator anim;
    public bool canBePressed;
    pinPad pinPad;
    private void Start()
    {
        anim = GetComponent<Animator>();
        pinPad = GetComponentInParent<pinPad>();
        canBePressed = true;
    }
    private void OnMouseDown()
    {
        if (!canBePressed)
        {
            return;
        }
        anim.Play("numpadbuttons");
        if (GameManager.instance.pinSolved) {
            return;
        }
        pinPad.buttonPushed(transform.GetSiblingIndex());
    }
}
