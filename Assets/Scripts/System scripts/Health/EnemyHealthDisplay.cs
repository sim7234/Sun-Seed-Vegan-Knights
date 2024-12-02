using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Health health;

    [SerializeField]
    private Color slightlyDamaged;

    [SerializeField]
    private Color damaged;

    [SerializeField]
    private Color almostDead;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
    }
   
    public void UpdateSprite()
    {
        if (health.GetCurrentHealth() / health.maxHealth <= 0.80f)
        {
            //Debug.Log(health.GetCurrentHealth() / health.maxHealth);
            //Debug.Log("SlightlyDamged");
            sprite.color = slightlyDamaged;
        }
        if (health.GetCurrentHealth() / health.maxHealth <= 0.50f)
        {
            sprite.color = damaged;
        }
        if (health.GetCurrentHealth() / health.maxHealth <= 0.30f)
        {
            sprite.color = almostDead;
        }
    }
}
