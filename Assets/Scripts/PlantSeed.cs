using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantSeed : MonoBehaviour
{
    [SerializeField]
    private GameObject seedType;

    private InputAction plantSeedAction;

    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            plantSeedAction = playerInput.actions["PlantSeed"]; 
        }
    }

    private void OnEnable()
    {
        if (plantSeedAction != null)
        {
            plantSeedAction.performed += OnPlantSeed;
            plantSeedAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (plantSeedAction != null)
        {
            plantSeedAction.performed -= OnPlantSeed;
            plantSeedAction.Disable();
        }
    }

    private void OnPlantSeed(InputAction.CallbackContext context)
    {
        Instantiate(seedType, transform.position, Quaternion.identity);
    }
}
