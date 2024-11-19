using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    public WeaponType type;

    public WeaponType GetWeaponType()
    {
        return type;
    }
}


public enum WeaponType
{
    Spear,
    Sword,
    Bomb,
    Turret  
}
