using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxWater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WaterSystem playerWater = collision.GetComponent<WaterSystem>();
        if (playerWater != null)
        {
            playerWater.maxWater = 200;
            playerWater.currentWater = 200;
        }
    }
}