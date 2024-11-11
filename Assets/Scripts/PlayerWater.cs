using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWater : MonoBehaviour
{

    [SerializeField]
    private int totalWater;
    [SerializeField]
    private int maxWater = 3;

    [SerializeField]
    private float waterGainTime;

    private float waterRateTimer;
    
    private List<Seed> seedInRange = new List<Seed>();

    private void Start()
    {
        totalWater = maxWater;
    }

    private void Update()
    {
        if(waterRateTimer >= waterGainTime && totalWater < maxWater)
        {
            waterRateTimer = 0;
            totalWater += 1;
        }
        else if (totalWater < maxWater)
        {
            waterRateTimer += Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && seedInRange.Count >= 1)
        {
            for (int i = 0; i < seedInRange.Count; i++)
            {
                if(seedInRange[i].WaterSeed(gameObject))
                {
                    break;
                }
            }
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
        if(newSeed != null)
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
