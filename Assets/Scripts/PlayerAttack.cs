using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            weaponAnimator.SetTrigger("PressedR1");
        }
    }
}