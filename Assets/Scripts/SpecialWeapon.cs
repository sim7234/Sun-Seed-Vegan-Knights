using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bigSword;
    [SerializeField]
    private GameObject bigSpear;

    private WeaponType weaponType;

    [SerializeField]
    private int specialWeaponAttacks = 5;
    private int attackCounter;

    [SerializeField]
    private GameObject baseWeapon;

    private List<GameObject> weaponPickupsInRange = new List<GameObject>();

    public float attackCooldown;

    private void Start()
    {
        attackCounter = 0;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (bigSword.activeSelf && attackCooldown <= 0)
            {
                attackCounter++;
                bigSword.GetComponent<Animator>().SetTrigger("Attack");
                attackCooldown = 1.5f;
                Debug.Log("Big sword swing");

                if (attackCounter >= specialWeaponAttacks)
                {
                    bigSword.SetActive(false);
                    bigSpear.SetActive(false);
                    baseWeapon.SetActive(true);
                }
            }
            else if (bigSpear.activeSelf && attackCooldown <= 0)
            {
                attackCounter++;
                bigSpear.GetComponent<Animator>().SetTrigger("Attack");
                attackCooldown = 1.5f;
                Debug.Log("Big spear swing");

                if (attackCounter >= specialWeaponAttacks)
                {
                    bigSpear.SetActive(false);
                    baseWeapon.SetActive(true);
                }
            }
            else if (weaponPickupsInRange.Count > 0)
            {
                attackCounter = 0;
                if (weaponType == WeaponType.Sword)
                {
                    bigSword.SetActive(true);
                    specialWeaponAttacks = 5;
                }
                else if (weaponType == WeaponType.Spear)
                {
                    bigSpear.SetActive(true);
                    specialWeaponAttacks = 10;
                }
                
                baseWeapon.SetActive(false);

                Destroy(weaponPickupsInRange[0]);
                weaponPickupsInRange.Clear();

                Debug.Log("Special weapon picked up!");
            }
        }
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponPickup pickup = collision.GetComponent<WeaponPickup>();
        if (pickup != null)
        {
            weaponPickupsInRange.Add(collision.gameObject);
            weaponType = pickup.type; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        WeaponPickup pickup = collision.GetComponent<WeaponPickup>();
        if (pickup != null)
        {
            weaponPickupsInRange.Remove(collision.gameObject);
        }
    }
}