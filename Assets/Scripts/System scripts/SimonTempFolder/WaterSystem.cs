using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WaterSystem : MonoBehaviour
{
    //TODO: Make 1 big water drop, that fills with water/blue color based on water amount

    //this script needs a "CanWater" script on any potential object you wish to be able to water
    //this script interacts with 

    int maxWater = 10;

   [SerializeField] int currentWater;

    float wateringCooldown = 0.35f;
    float wateringTimer;

    //this is what decides how often water is regained
    float baseWaterRefillCooldown = 10f;

    //this value can be changed by other scripts. Mainly "NaturalWater" script.
    [HideInInspector] public float waterRefillCooldown;
    float waterRefillTimer;

    bool hasEnoughWater;

    private InputAction waterAction;

    private List<Seed> seedInRange = new List<Seed>();

    int payWater;

    bool buttonHeld;

    [SerializeField] RectTransform uiElement;

    Camera camera;

    Vector3 screenPos;

    void Start()
    {
        camera = Camera.main;
        screenPos = camera.WorldToScreenPoint(transform.position);
        uiElement.transform.position = screenPos + new Vector3 (0,10,0);

        waterRefillCooldown = baseWaterRefillCooldown;
        currentWater = maxWater;
        waterRefillTimer = waterRefillCooldown;
    }
    void Update()
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
    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            waterAction = playerInput.actions["Water"];
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
        if (other.GetComponent<CanWater>() != null && buttonHeld == true && wateringTimer <= 0 && other.GetComponent<CanWater>().canBeWatered)
        {
            wateringTimer = wateringCooldown;
            Debug.Log("Watered Seed");

            payWater = other.GetComponent<CanWater>().waterCostPerAction;

            hasEnoughWater = TakeWater(payWater);
            if (hasEnoughWater == true)
            {
                other.GetComponent<CanWater>().currentWater += payWater;
            }
        }
    }

    public void OnWater(InputAction.CallbackContext context)
    {
        //WaterInput triggers on press and release.
        if (context.performed)
        {
            buttonHeld = !buttonHeld;
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

    public int GetCurrentWater()
    {
        return currentWater;
    }

    void RefillAllWater()
    {
        currentWater = maxWater;
    }

    public void ChangeWaterRefillRate(float rate)
    {
        waterRefillCooldown = rate;

        if (rate == 0)
        {
            waterRefillCooldown = baseWaterRefillCooldown;
        }
    }

}

