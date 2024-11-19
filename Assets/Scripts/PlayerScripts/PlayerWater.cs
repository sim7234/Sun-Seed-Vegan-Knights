using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerWater : MonoBehaviour
{
    [SerializeField]
    private int totalWater;
    [SerializeField]
    private int maxWater = 3;

    [SerializeField]
    private float waterGainTime;

    private float waterRateTimer;

    [SerializeField]
    private List<GameObject> waterDropsDisplay = new List<GameObject>();
    private List<Seed> seedInRange = new List<Seed>();

    private InputAction waterAction;

    [SerializeField]
    private UnityEvent onWaterPerformed;
    [SerializeField]
    private UnityEvent onWaterGained;

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

    private void Start()
    {
        totalWater = maxWater;
        UpdateWaterDropDisplay();
    }

    private void Update()
    {
        if (waterRateTimer >= waterGainTime && totalWater < maxWater)
        {
            waterRateTimer = 0;
            totalWater += 1;
            onWaterGained?.Invoke(); 
        }
        else if (totalWater < maxWater)
        {
            waterRateTimer += Time.deltaTime;
        }

        UpdateWaterDropDisplay();
    }

    public void OnWater(InputAction.CallbackContext context)
    {
        if (seedInRange.Count >= 1)
        {
            for (int i = 0; i < seedInRange.Count; i++)
            {
                if (seedInRange[i].WaterSeed(gameObject))
                {
                    onWaterPerformed?.Invoke(); 
                    break;
                }
            }
        }
    }

    public void UpdateWaterDropDisplay()
    {
        foreach (var item in waterDropsDisplay)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < totalWater; i++)
        {
            waterDropsDisplay[i].SetActive(true);
        }
    }

    public void TakeWater(int waterCost)
    {
        totalWater -= waterCost;
    }

    public int TotalWater()
    {
        return totalWater;
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
}