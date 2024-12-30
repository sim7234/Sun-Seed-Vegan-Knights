using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantSeed : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> seedTypes = new List<GameObject>();
    private List<SeedSystem> seedInRange = new List<SeedSystem>();

    [SerializeField]
    private WeaponType currentType;

    [SerializeField]
    private GameObject currentSeedTypeInRange;

    private PlayerInput playerInput;
    private InputAction plantSeedAction;
    private InputAction pickUpSeed;

    public float plantSpeed = 1;
    public float plantingTimer;

    private float plantingCooldown = 0.2f; // cd before u can water
    private bool recentlyPlanted = false;

    [HideInInspector]
    public bool inSun;

    [SerializeField]
    private TextMeshProUGUI seedCooldownText;

    [SerializeField]
    private GameObject inSunEffect;

    [HideInInspector]
    public bool cantPlant;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        plantSeedAction = playerInput.actions["PlantSeed"];
        pickUpSeed = playerInput.actions["PickUp"];

    }
    private void Start()
    {
        Invoke(nameof(SelectSeed), 0.2f);

        int playerIndex = GetComponent<PlayerMovement>().playerIndex;
        if (playerIndex == 1)
        {
            currentType = SaveData.Instance.seedType;
            //Debug.Log("player 1 seed selected");
        }
        else if (playerIndex == 2)
        {
            currentType = SaveData.Instance.seedType2;
            //Debug.Log("player 2 seed selected");
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
    private void Update()
    {
        if (plantingTimer <= 0)
        {
            plantingTimer = 0;
            seedCooldownText.text = Mathf.RoundToInt(plantingTimer).ToString();
        }
        else
        {
            plantingTimer -= Time.deltaTime;
            seedCooldownText.text = Mathf.RoundToInt(plantingTimer).ToString();
        }

        if (inSun)
        {
            inSunEffect.SetActive(true);
        }
        else
        {
            inSunEffect.SetActive(false);
        }
    }
    private void SelectSeed()
    {
        if (currentSeedTypeInRange != null)
        {
            Debug.Log("New seed pickup");
            currentType = currentSeedTypeInRange.GetComponent<WeaponPickup>().type;
            
        }
    }
    private void OnEnable()
    {
        plantSeedAction.performed += OnPlantSeedPerformed;
        pickUpSeed.performed += OnSeedPickup;
    }

    private void OnDisable()
    {
        plantSeedAction.performed -= OnPlantSeedPerformed;
        pickUpSeed.performed -= OnSeedPickup;
    }

    private void OnPlantSeedPerformed(InputAction.CallbackContext context)
    {
        if (plantingTimer <= 0 && GetComponent<PlayerWater>().GetSeedsInRangeCount() == 0 && currentSeedTypeInRange == null && inSun && !cantPlant)
        {
            recentlyPlanted = true;
            StartCoroutine(ResetPlantingFlag());

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
            plantingTimer = plantSpeed;
        }
    }
    public void OnSeedPickup(InputAction.CallbackContext context)
    {
        SelectSeed();
    }

    private IEnumerator ResetPlantingFlag()
    {
        yield return new WaitForSeconds(plantingCooldown);
        recentlyPlanted = false;
    }

    public bool IsRecentlyPlanted()
    {
        return recentlyPlanted;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<WeaponPickup>() != null && !other.gameObject.CompareTag("WeaponPickup"))
        {
            currentSeedTypeInRange = other.gameObject;
        }

        SeedSystem newSeed = other.GetComponent<SeedSystem>();
        if (newSeed != null)
        {
            newSeed.DisplayCost(true);
            seedInRange.Add(newSeed);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<WeaponPickup>() != null && !other.gameObject.CompareTag("WeaponPickup"))
        {
            if (currentSeedTypeInRange == other.gameObject)
            {
                currentSeedTypeInRange = null;
            }
        }

        SeedSystem oldSeed = other.GetComponent<SeedSystem>();
        if (oldSeed != null)
        {
            oldSeed.DisplayCost(false);
            seedInRange.Remove(oldSeed);
        }
    }
}