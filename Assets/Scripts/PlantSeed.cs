using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class PlantSeed : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> seedTypes = new List<GameObject>(); 

    [SerializeField]
    private WeaponType currentType; 

    
    public void OnPlantSeed(InputAction.CallbackContext context)
    {
        if (context.performed) 
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponPickup pickup = collision.GetComponent<WeaponPickup>();
        if (pickup != null)
        {
            currentType = pickup.type;
        }
    }
}