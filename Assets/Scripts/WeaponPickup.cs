using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    public WeaponType type;

    [SerializeField]
    private GameObject weaponPickupText;

    private List<GameObject> playersInRange = new List<GameObject>();

    public WeaponType GetWeaponType()
    {
        SaveData.Instance.seedType = type;

        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Player"))
        {
            playersInRange.Add(gameObject);
        }
        weaponPickupText.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
     
        if(playersInRange.Contains(collision.gameObject))
        {
            playersInRange.Remove(collision.gameObject);
        }
        if(playersInRange.Count <= 0)
        {
            weaponPickupText.SetActive(false);
        }
    }
    
}

public enum WeaponType
{
    Spear,
    Sword,
    Bomb,
    Turret  
}
