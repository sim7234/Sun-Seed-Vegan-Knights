using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallWaterDrain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WaterSystem playerWater = collision.GetComponent<WaterSystem>();
        if (playerWater != null)
        {
            playerWater.currentWater = 0;
            Debug.Log("Drained water");
        }
    }
}