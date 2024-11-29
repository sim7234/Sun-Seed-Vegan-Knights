using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    public WeaponType type;

    [SerializeField]
    private GameObject weaponPickupText;

    [SerializeField]
    private List<GameObject> playersInRange = new List<GameObject>();

    public WeaponType GetWeaponType()
    {
        SaveData.Instance.seedType = type;

        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponPickupText != null)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            {
                playersInRange.Add(collision.gameObject);
            }
            weaponPickupText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (weaponPickupText != null)
        {
            if (playersInRange.Contains(collision.gameObject))
            {
                playersInRange.Remove(collision.gameObject);
            }
            if (playersInRange.Count <= 0)
            {
                weaponPickupText.SetActive(false);
            }
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
