using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreakWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpecialWeapon specialWeapon = collision.GetComponent<SpecialWeapon>();
        if (specialWeapon != null && specialWeapon.IsWieldingSword())
        {
            specialWeapon.DisableSpecialWeapon();
        }
    }
}