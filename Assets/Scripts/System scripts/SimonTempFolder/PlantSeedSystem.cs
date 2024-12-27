using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantSeedSystem : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> seedTypes = new List<GameObject>();

    [SerializeField]
    private WeaponType currentType;

    [SerializeField]
    private GameObject currentSeedTypeInRange;

    private PlayerInput playerInput;
    private InputAction plantSeedAction;
    private InputAction pickUpSeed;

    public float plantSpeed = 30;
    public float plantingTimer;

    private float plantingCooldown = 0.2f;
    private bool recentlyPlanted = false;

    [SerializeField]
    private TextMeshProUGUI seedCooldownText;

    [HideInInspector] public bool cantPlant;

    private List<SeedSystem> seedInRange = new List<SeedSystem>();

    EnteredSun inSunScript;

    bool canPlant = true;

    [SerializeField]
    private AudioClip weaponPickupSound;

    [SerializeField]
    private List<GameObject> pickUpPs = new List<GameObject>();


    private void Awake()
    {
        inSunScript = GetComponent<EnteredSun>();
        playerInput = GetComponent<PlayerInput>();
        plantSeedAction = playerInput.actions["PlantSeed"];
        pickUpSeed = playerInput.actions["PickUp"];

    }
    private void Start()
    {
        plantingTimer = plantSpeed;
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

        StartCoroutine(CleanReapting());
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

       
    }
    private void CleanSeed()
    {
        for (int i = 0; seedInRange.Count > i; i++)
        {
            if (seedInRange[i] == null)
            {
                seedInRange.RemoveAt(i);
            }
        }
        if (currentSeedTypeInRange != null)
        {
            if (Vector3.Distance(transform.position, currentSeedTypeInRange.transform.position) <= 5)
            {
                currentSeedTypeInRange = null;
                
            }
        }
    }

    private IEnumerator CleanReapting()
    {
        while (true)
        {
            CleanSeed();
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void SelectSeed()
    {
        if (currentSeedTypeInRange != null)
        {
            GetComponent<AudioSource>().PlayOneShot(weaponPickupSound);
            currentType = currentSeedTypeInRange.GetComponent<WeaponPickup>().type;
            switch(currentType)
            {
                case WeaponType.Spear:
                    GameObject newSperaPickupPS = Instantiate(pickUpPs[0], transform.position, Quaternion.identity);
                    Destroy(newSperaPickupPS, 2);
                    break;  
                case WeaponType.Sword:
                    GameObject newSwordPickupPS = Instantiate(pickUpPs[1], transform.position, Quaternion.identity);
                    Destroy(newSwordPickupPS, 2);
                    break;

                default:
                    Debug.Log(currentType.ToString() + " Does not exist as seedPrefab");
                    break;
            }
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
        if (plantingTimer > 0)
        {
            Debug.Log("Planting timer cooldown");
        }
        if (seedInRange.Count != 0)
        {
            Debug.Log("To many seeds in range");
        }
        if (currentSeedTypeInRange != null)
        {
            Debug.Log("Current seed type in range is not null");
        }
        if (inSunScript.inSun == false)
        {
            Debug.Log("Not in sun");
        }
        if (cantPlant)
        {
            Debug.Log("Cant plant bool");
        }



        if (plantingTimer <= 0 && seedInRange.Count == 0 && currentSeedTypeInRange == null && inSunScript.inSun && !cantPlant)
        {
            recentlyPlanted = true;

            //this makes sure you cant water a seed at the same time you plant.
            if (GetComponent<WaterSystem>() != null)
            {
                GetComponent<WaterSystem>().wateringTimer = 0.10f;
            }

            StartCoroutine(ResetPlantingFlag());


            switch (currentType)
            {
                case WeaponType.Spear:
                    Instantiate(seedTypes[0], transform.position, Quaternion.identity);
                    break;
                case WeaponType.Sword:
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SeedSystem newSeed = collision.GetComponent<SeedSystem>();
        if (newSeed != null)
        {
            newSeed.DisplayCost(true);
            seedInRange.Add(newSeed);
        }

        if (collision.GetComponent<WeaponPickup>() != null && !collision.gameObject.CompareTag("WeaponPickup"))
        {
            currentSeedTypeInRange = collision.gameObject;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SeedSystem oldSeed = collision.GetComponent<SeedSystem>();
        if (oldSeed != null)
        {
            oldSeed.DisplayCost(false);
            seedInRange.Remove(oldSeed);
        }

        //currentSeedTypeInRange != null; problem
        if (collision.GetComponent<WeaponPickup>() != null && !collision.gameObject.CompareTag("WeaponPickup"))
        {
            if (currentSeedTypeInRange == collision.gameObject)
            {
                currentSeedTypeInRange = null;
            }
        }
    }

}