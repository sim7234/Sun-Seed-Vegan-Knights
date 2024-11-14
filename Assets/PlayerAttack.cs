using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    private Collider2D weaponCollider;

    private Animator weaponAnimator;

    private void Start()
    {
        weaponCollider = weapon.GetComponent<Collider2D>();
        weaponAnimator = weapon.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            weaponAnimator.SetTrigger("PressedR1");
        }
    }

}
