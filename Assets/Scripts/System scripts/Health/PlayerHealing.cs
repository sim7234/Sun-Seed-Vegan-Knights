using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealing : MonoBehaviour
{
    private Health health;
    [SerializeField]
    private float healingAmount;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if(GetComponent<PlantSeed>().inSun && GetComponent<Rigidbody2D>().velocity.magnitude <= 1)
        {
            health.Heal(healingAmount * Time.deltaTime);
        }
    }
}
