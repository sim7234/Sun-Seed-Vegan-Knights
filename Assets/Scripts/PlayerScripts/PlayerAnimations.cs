using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GridBrushBase;

public class PlayerAnimations : MonoBehaviour
{

    [Header("Running")]
    [SerializeField]
    private GameObject moveRight;

    [SerializeField]
    private GameObject moveLeft;

    [SerializeField]
    private GameObject moveUp;

    [SerializeField]
    private GameObject moveDown;

    [Space]
    [Header("Idle")]
    [SerializeField]
    private GameObject idleRight;

    [SerializeField]
    private GameObject idleLeft;

    [SerializeField]
    private GameObject idleUp;

    [SerializeField]
    private GameObject idleDown;

    [Space]
    [Header("Attacking")]
    [SerializeField]
    private GameObject attackRight;

    [SerializeField]
    private GameObject attackLeft;

    [SerializeField]
    private GameObject attackUp;

    [SerializeField]
    private GameObject attackDown;

    [Space]
    [Header("Special Attacking")]
    [SerializeField]
    private GameObject sAttackRight;

    [SerializeField]
    private GameObject sAttackLeft;

    [SerializeField]
    private GameObject sAttackUp;

    [SerializeField]
    private GameObject sAttackDown;


    private Rigidbody2D rb2d;

    private bool attacking;

    [SerializeField]
    private GameObject basicSword;

    [SerializeField]
    private GameObject directionalArrow;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb2d.velocity.normalized.y, rb2d.velocity.normalized.x) * Mathf.Rad2Deg;

        if (!attacking)
        {

            if (rb2d.velocity.magnitude > 0.4f)
            {
                running(angle);
            }
            else
            {
                if(rb2d.velocity.magnitude > 0.01f)
                {
                    Idle(angle);
                }
            }
        }
        else
        {
            float aimAngle = (directionalArrow.transform.localEulerAngles.z % 360) + 90;
            if(aimAngle < 0)
            {
                aimAngle = -aimAngle;
            }
            Attacking(aimAngle);
            //Attacking(angle);
        }
    }

    private void running(float angle)
    {
        idleRight.SetActive(false);
        idleLeft.SetActive(false);
        idleDown.SetActive(false);
        idleUp.SetActive(false);

        attackUp.SetActive(false);
        attackDown.SetActive(false);
        attackRight.SetActive(false);
        attackLeft.SetActive(false);

        sAttackUp.SetActive(false);
        sAttackDown.SetActive(false);
        sAttackRight.SetActive(false);
        sAttackLeft.SetActive(false);

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

        attackUp.SetActive(false);
        attackDown.SetActive(false);
        attackRight.SetActive(false);
        attackLeft.SetActive(false);

        sAttackUp.SetActive(false);
        sAttackDown.SetActive(false);
        sAttackRight.SetActive(false);
        sAttackLeft.SetActive(false);

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

    public void StartAttack(float timeBeforeRest)
    {
        attacking = true;
        Invoke(nameof(attackingOver), timeBeforeRest);
    }
    private void Attacking(float angle)
    {
        idleRight.SetActive(false);
        idleLeft.SetActive(false);
        idleDown.SetActive(false);
        idleUp.SetActive(false);

        moveUp.SetActive(false);
        moveDown.SetActive(false);
        moveRight.SetActive(false);
        moveLeft.SetActive(false);
        //Basic attack or specal weaponss
        if (basicSword.activeSelf)
        {
            if (angle > 45 && angle < 135)
            {
                attackRight.SetActive(false);
                attackLeft.SetActive(false);
                attackDown.SetActive(false);

                attackUp.SetActive(true);
            }
            else if (angle > 135 && angle < 225)
            {
                attackUp.SetActive(false);
                attackDown.SetActive(false);
                attackRight.SetActive(false);

                attackLeft.SetActive(true);
            }
            else if (angle > 225 && angle < 270)
            {
                attackRight.SetActive(false);
                attackLeft.SetActive(false);
                attackUp.SetActive(false);

                attackDown.SetActive(true);
            }
            else
            {
                attackLeft.SetActive(false);
                attackUp.SetActive(false);
                attackDown.SetActive(false);

                attackRight.SetActive(true);
            }

        }
        else
        {
            if (angle > 45 && angle < 135)
            {
                sAttackRight.SetActive(false);
                sAttackLeft.SetActive(false);
                sAttackDown.SetActive(false);

                sAttackUp.SetActive(true);
            }
            else if (angle > 135 && angle < 225)
            {
                sAttackUp.SetActive(false);
                sAttackDown.SetActive(false);
                sAttackRight.SetActive(false);

                sAttackLeft.SetActive(true);
            }
            else if (angle > 225 && angle < 270)
            {
                sAttackRight.SetActive(false);
                sAttackLeft.SetActive(false);
                sAttackUp.SetActive(false);

                sAttackDown.SetActive(true);
            }
            else
            {
                sAttackLeft.SetActive(false);
                sAttackUp.SetActive(false);
                sAttackDown.SetActive(false);

                sAttackRight.SetActive(true);
            }
        }

    }

    private void attackingOver()
    {
        attacking = false;
        //if(basicSword.activeSelf)
        //{
        //    Idle(-90);
        //}
    }

}
