using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bigSword;
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
    private void Update()
    {
        if (attackCounter >= specialWeaponAttacks)
        {
            bigSword.SetActive(false);
            baseWeapon.SetActive(true);
        }
        if (bigSword.activeSelf && Input.GetKeyDown(KeyCode.Joystick1Button5) && attackCooldown < 0)
        {
            attackCounter++;
            bigSword.GetComponent<Animator>().SetTrigger("Attack");
            attackCooldown = 1.5f;

            Debug.Log("Big swing");
        }
        else if (weaponPickupsInRange.Count > 0 && Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            attackCounter = 0;
            bigSword.SetActive(true);
            baseWeapon.SetActive(false);
            Destroy(weaponPickupsInRange[0]);
            weaponPickupsInRange.Clear();
        }
        attackCooldown -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<WeaponPickup>() != null)
        {
            weaponPickupsInRange.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<WeaponPickup>() != null)
        {
            weaponPickupsInRange.Remove(collision.gameObject);
        }
    }
}
