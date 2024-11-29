using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallWaterDrain : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerWater playerWater = collision.GetComponent<PlayerWater>();
        if (playerWater != null)
        {
            playerWater.TakeAllWater();
        }
    }
}