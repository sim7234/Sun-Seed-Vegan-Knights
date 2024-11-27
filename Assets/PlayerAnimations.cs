using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private Rigidbody2D rb2d;

    
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb2d.velocity.normalized.y, rb2d.velocity.normalized.x) * Mathf.Rad2Deg;
        
        if(angle > -45 && angle < 45)
        {
            moveLeft.SetActive(false);
            moveUp.SetActive(false);
            moveDown.SetActive(false);

            moveRight.SetActive(true);
        }
        else if(angle > 45 && angle < 135)
        {
            moveRight.SetActive(false);
            moveLeft.SetActive(false);
            moveDown.SetActive(false);

            moveUp.SetActive(true);
        }
        else if(angle > 135 && angle < 180 || angle < -135 && angle > -180)
        {
            moveUp.SetActive(false);
            moveDown.SetActive(false);
            moveRight.SetActive(false);

            moveLeft.SetActive(true);
        }
        else if(angle > -135 && angle < -45)
        {
            moveRight.SetActive(false);
            moveLeft.SetActive(false);
            moveUp.SetActive(false);


            moveDown.SetActive(true);
        }
        
    }


}
