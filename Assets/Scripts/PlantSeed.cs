using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlantSeed : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> seedTypes = new List<GameObject>();

    [SerializeField]
    private WeaponType currentType;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            switch(currentType)
            {
                case WeaponType.Sword:
                    Instantiate(seedTypes[0], transform.position, Quaternion.identity);
                    break;

                case WeaponType.Spear:
                    Instantiate(seedTypes[1], transform.position, Quaternion.identity);
                    break;
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<WeaponPickup>() != null)
        {
            currentType = collision.GetComponent<WeaponPickup>().type;
        }
    }
}
