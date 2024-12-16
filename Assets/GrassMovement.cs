using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMovement : MonoBehaviour
{
    Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("OnTouch");
    }
}
