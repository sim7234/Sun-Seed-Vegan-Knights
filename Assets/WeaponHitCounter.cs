using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitCounter : MonoBehaviour
{
    [SerializeField]
    SpecialWeapon playerSpecialScript;

    private bool hasHit;

    private float timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (timer <= 0)
        {
            hasHit = false;
        }
        if (collision.CompareTag("Enemy") && hasHit == false)
        {
            hasHit = true;
            timer = 0.5f;

            playerSpecialScript.attackCounter++;
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        
    }
}
