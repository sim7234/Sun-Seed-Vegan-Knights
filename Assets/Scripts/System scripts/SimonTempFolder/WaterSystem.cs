using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WaterSystem : MonoBehaviour
{
    //this script needs a "CanWater" script on any potential object you wish to be able to water
    //this script interacts with NaturalWater, PlantSeedSystem.

    float maxWater = 10.0f;

    [SerializeField] float currentWater;

    float wateringCooldown = 0.35f;
    //this value is changed from PlantSeedSystem to make sure you cant water at the same time you plant.
    [HideInInspector] public float wateringTimer;

    float baseWaterRefillCooldown = 10f;
    //these values can be changed by other scripts. Mainly "NaturalWater" script.
    [HideInInspector] public float waterRefillCooldown;
    [HideInInspector] public float waterRefillTimer;

    float waterPercentage;

    int payWater;

    bool hasEnoughWater;
    bool waterButtonHeld;

    private InputAction waterAction;

    private List<Seed> seedInRange = new List<Seed>();

    [SerializeField] Image waterOutLineImage;
    [SerializeField] Image playersWaterImage;

    void Start()
    {
        waterRefillCooldown = baseWaterRefillCooldown;
        currentWater = maxWater;
        waterRefillTimer = waterRefillCooldown;
    }
    void Update()
    {
        waterOutLineImage.transform.position = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y 
            + gameObject.transform.localScale.y + 0.3f,0));

        changeImageFill();
        refillWater();      
    }
    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            waterAction = playerInput.actions["Water"];
        }
    }

    void changeImageFill()
    {
        waterPercentage = (currentWater / maxWater);

        if (playersWaterImage.fillAmount > waterPercentage)
        {
            playersWaterImage.fillAmount -= Time.deltaTime * 0.4f;
        }

        if (playersWaterImage.fillAmount < waterPercentage)
        {
            playersWaterImage.fillAmount += Time.deltaTime * 0.2f;
        }
    }
    void refillWater()
    {
        if (wateringTimer > 0)
        {
            wateringTimer -= Time.deltaTime;
        }

        if (waterRefillTimer > 0)
        {
            waterRefillTimer -= Time.deltaTime;
        }

        if (waterRefillTimer <= 0 && currentWater < maxWater)
        {
            currentWater++;
            waterRefillTimer = waterRefillCooldown;
        }
    }

    private void OnEnable()
    {
        if (waterAction != null)
        {
            waterAction.performed += OnWater;
            waterAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (waterAction != null)
        {
            waterAction.performed -= OnWater;
            waterAction.Disable();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<CanWater>() != null && other.GetComponent<CanWater>().enabled == true)
        {
            DisplayWater(true);
        }

        if (other.GetComponent<CanWater>() != null && waterButtonHeld == true && wateringTimer <= 0 && other.GetComponent<CanWater>().canBeWatered)
        {
            wateringTimer = wateringCooldown;

            payWater = other.GetComponent<CanWater>().waterCostPerAction;

            hasEnoughWater = TakeWater(payWater);
            if (hasEnoughWater == true)
            {
                other.GetComponent<CanWater>().currentWater += payWater;
            }
        }

        if (other.GetComponent<NaturalWater>() != null)
        {
            DisplayWater(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NaturalWater>() != null)
        {
            DisplayWater(false);
        }

        if (other.GetComponent<CanWater>() != null)
        {
            DisplayWater(false);
        }
    }

    public void OnWater(InputAction.CallbackContext context)
    {
        //WaterInput triggers on press and release.
        if (context.performed)
        {
            waterButtonHeld = !waterButtonHeld;
        }
    }

    /// <summary>
    /// Takes a water cost, returns true if player had enough water and had the cooldown to water, returns false otherwise.
    /// </summary>
    /// <param name="waterCost"></param>
    /// <returns></returns>
    public bool TakeWater(int waterCost)
    {
        if (currentWater >= waterCost)
        {
            currentWater -= waterCost;
            return true;
        }
        else
        {
            return false;
        }
    }   

    void DisplayWater(bool displayWater)
    {
        waterOutLineImage.enabled = displayWater;
        playersWaterImage.enabled = displayWater;
    }

    public void ChangeWaterRefillRate(float rate)
    {
        waterRefillCooldown = rate;

        if (rate == 0)
        {
            waterRefillCooldown = baseWaterRefillCooldown;
        }
    }

    public float GetCurrentWater()
    {
        return currentWater;
    }

    void RefillAllWater()
    {
        currentWater = maxWater;
    }
}

