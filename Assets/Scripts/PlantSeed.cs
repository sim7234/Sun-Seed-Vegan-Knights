using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlantSeed : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> seedTypes = new List<GameObject>();

    [SerializeField]
    private WeaponType currentType;

    [SerializeField]
    private UnityEvent onPlantSeed;

    public void PlantSeedAction()
    {
        switch (currentType)
        {
            case WeaponType.Sword:
                Instantiate(seedTypes[0], transform.position, Quaternion.identity);
                break;
            case WeaponType.Spear:
                Instantiate(seedTypes[1], transform.position, Quaternion.identity);
                break;
            case WeaponType.Turret:
                Instantiate(seedTypes[2], transform.position, Quaternion.identity);
                break;
            case WeaponType.Bomb:
                Instantiate(seedTypes[3], transform.position, Quaternion.identity);
                break;
        }

        onPlantSeed?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var weaponPickup = collision.GetComponent<WeaponPickup>();
        if (weaponPickup != null)
        {
            currentType = weaponPickup.type;
        }
    }
}