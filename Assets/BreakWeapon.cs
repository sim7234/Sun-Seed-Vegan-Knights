using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
        {
            SpecialWeapon specialWeapon = collision.GetComponent<SpecialWeapon>();
            if (specialWeapon != null && specialWeapon.IsWieldingSword())
            {
                specialWeapon.DisableSpecialWeapon();
            }
            else if (specialWeapon != null && specialWeapon.IsWieldingSpear())
            {
                specialWeapon.DisableSpecialWeapon();
            }
        }
}
