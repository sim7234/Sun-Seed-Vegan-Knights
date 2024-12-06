using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RmoveSeedCooldown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlantSeedSystem plantSeedSystem = collision.GetComponent<PlantSeedSystem>();

        if (plantSeedSystem != null)
        {
            plantSeedSystem.plantingTimer = 0;
            Debug.Log("Planting cooldown removed!");
        }
    }
}