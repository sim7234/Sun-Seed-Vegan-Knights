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
    private void Start()
    {
        Invoke(nameof(SelectSeed), 0.2f);
    }
    private void SelectSeed()
    {
        int playerIndex = GetComponent<PlayerMovement>().playerIndex;
        if (playerIndex == 1)
        {
            currentType = SaveData.Instance.seedType;
            Debug.Log("player 1 seed selected");
        }
        else if (playerIndex == 2)
        {
            currentType = SaveData.Instance.seedType2;
            Debug.Log("player 2 seed selected");
        }
        else if (playerIndex == 3)
        {
            currentType = SaveData.Instance.seedType3;
        }
        else if (playerIndex == 4)
        {
            currentType = SaveData.Instance.seedType4;
        }
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
            int playerIndex = GetComponent<PlayerMovement>().playerIndex;
            if ( playerIndex == 1)
            {
                SaveData.Instance.seedType = currentType;
            }
            else if (playerIndex == 2)
            {
                SaveData.Instance.seedType2 = currentType;
            }
            else if (playerIndex == 3)
            {
                SaveData.Instance.seedType3 = currentType;
            }
            else if (playerIndex == 4)
            {
                SaveData.Instance.seedType4 = currentType;
            }
        }
    }
}