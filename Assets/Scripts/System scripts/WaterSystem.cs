using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WaterSystem : MonoBehaviour
{

    //TODO: Make 1 big water drop, that fills with water/blue color based on water amount
    

    int maxWater = 10;

   [HideInInspector] public int currentWater;

    float wateringTime = 0.1f;
    float wateringTimer;

    float waterRefillCooldown = 1.5f;
    float waterRefillTimer;

    bool hasEnoughWater;

    private InputAction waterAction;

    private List<Seed> seedInRange = new List<Seed>();

    private PlantSeed plantSeed;

    //[SerializeField] SpriteRenderer waterImage;

    // Start is called before the first frame update
    void Start()
    {
        currentWater = maxWater;
        waterRefillTimer = waterRefillCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (wateringTimer > 0)
        {
            wateringTimer--;
        }
        else

        if (waterRefillTimer > 0)
        {
            waterRefillTimer -= Time.deltaTime;
        }

        if (waterRefillCooldown <= 0 && currentWater < maxWater)
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

        plantSeed = GetComponent<PlantSeed>();
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

    public void OnWater(InputAction.CallbackContext context)
    {
        if (plantSeed != null && plantSeed.IsRecentlyPlanted())
        {
            return;
        }

        if (seedInRange.Count >= 1)
        {
            for (int i = 0; i < seedInRange.Count; i++)
            {
                if (seedInRange[i].WaterSeed(gameObject))
                {
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Takes a water cost, returns true if player had enough water and had the cooldown to water, returns false otherwise.
    /// </summary>
    /// <param name="waterCost"></param>
    /// <returns></returns>
    public bool TakeWater(int waterCost)
    {
        if (currentWater > waterCost && wateringTimer <= 0)
        {
            wateringTimer = wateringTime;
            currentWater -= waterCost;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Seed newSeed = collision.GetComponent<Seed>();
        if (newSeed != null)
        {
            newSeed.DisplayCost();
            seedInRange.Add(newSeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Seed oldSeed = collision.GetComponent<Seed>();
        if (oldSeed != null)
        {
            oldSeed.DisplayCost();
            seedInRange.Remove(oldSeed);
        }
    }
    public int GetCurrentWater()
    {
        return currentWater;
    }

    public int GetSeedsInRangeCount()
    {
        return seedInRange.Count;
    }

    void RefillAllWater()
    {
        currentWater = maxWater;
    }

}

