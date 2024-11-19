using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantSeed : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> seedTypes = new List<GameObject>();

    [SerializeField]
    private WeaponType currentType;

    private PlayerInput playerInput;
    private InputAction plantSeedAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        plantSeedAction = playerInput.actions["PlantSeed"];
    }

    private void OnEnable()
    {
        plantSeedAction.performed += OnPlantSeedPerformed;
    }

    private void OnDisable()
    {
        plantSeedAction.performed -= OnPlantSeedPerformed;
    }

    private void OnPlantSeedPerformed(InputAction.CallbackContext context)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<WeaponPickup>() != null)
        {
            currentType = collision.GetComponent<WeaponPickup>().type;
        }
    }
}