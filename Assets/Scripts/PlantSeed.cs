using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    [SerializeField]
    private GameObject seedType;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Instantiate(seedType, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<WeaponPickup>() != null)
        {
            if(collision.GetComponent<WeaponPickup>().type == WeaponType.Sword)
            {
                //Sword
            }
            if (collision.GetComponent<WeaponPickup>().type == WeaponType.Spear)
            {
                //Spear
            }
        }
    }
}
