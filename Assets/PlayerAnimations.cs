using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GridBrushBase;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    private GameObject moveRight;

    [SerializeField]
    private GameObject moveLeft;

    [SerializeField]
    private GameObject moveUp;
    
    [SerializeField]
    private GameObject moveDown;

    [SerializeField]
    private GameObject idleRight;

    [SerializeField]
    private GameObject idleLeft;

    [SerializeField]
    private GameObject idleUp;

    [SerializeField]
    private GameObject idleDown;


    private Rigidbody2D rb2d;

    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb2d.velocity.normalized.y, rb2d.velocity.normalized.x) * Mathf.Rad2Deg;

        if(rb2d.velocity.magnitude > 0.4f)
        {
            running(angle);
        }
        else
        {
            Idle(angle);
        }
    }

    private void running(float angle)
    {
        idleRight.SetActive(false);
        idleLeft.SetActive(false);
        idleDown.SetActive(false);
        idleUp.SetActive(false);

        if (angle > -45 && angle < 45)
        {
            moveLeft.SetActive(false);
            moveUp.SetActive(false);
            moveDown.SetActive(false);

            moveRight.SetActive(true);
        }
        else if (angle > 45 && angle < 135)
        {
            moveRight.SetActive(false);
            moveLeft.SetActive(false);
            moveDown.SetActive(false);

            moveUp.SetActive(true);
        }
        else if (angle > 135 && angle < 180 || angle < -135 && angle > -180)
        {
            moveUp.SetActive(false);
            moveDown.SetActive(false);
            moveRight.SetActive(false);

            moveLeft.SetActive(true);
        }
        else if (angle > -135 && angle < -45)
        {
            moveRight.SetActive(false);
            moveLeft.SetActive(false);
            moveUp.SetActive(false);

            moveDown.SetActive(true);
        }
    }

    private void Idle(float angle)
    {
        moveUp.SetActive(false);
        moveDown.SetActive(false);
        moveRight.SetActive(false);
        moveLeft.SetActive(false);
        
        if (angle > -45 && angle < 45)
        {
            idleLeft.SetActive(false);
            idleUp.SetActive(false);
            idleDown.SetActive(false);

            idleRight.SetActive(true);
        }
        else if (angle > 45 && angle < 135)
        {
            idleRight.SetActive(false);
            idleLeft.SetActive(false);
            idleDown.SetActive(false);

            idleUp.SetActive(true);
        }
        else if (angle > 135 && angle < 180 || angle < -135 && angle > -180)
        {
            idleUp.SetActive(false);
            idleDown.SetActive(false);
            idleRight.SetActive(false);

            idleLeft.SetActive(true);
        }
        else if (angle > -135 && angle < -45)
        {
            idleRight.SetActive(false);
            idleLeft.SetActive(false);
            idleUp.SetActive(false);

            idleDown.SetActive(true);
        }
    }
}
