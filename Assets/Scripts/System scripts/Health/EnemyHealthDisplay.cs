using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> sprites;
    private Health health;

    [SerializeField]
    private Color slightlyDamaged;

    [SerializeField]
    private Color damaged;

    [SerializeField]
    private Color almostDead;

    private void Start()
    {
        health = GetComponent<Health>();
    }
   
    public void UpdateSprite()
    {
        if (health.GetCurrentHealth() / health.maxHealth <= 0.80f)
        {
            //Debug.Log(health.GetCurrentHealth() / health.maxHealth);
            //Debug.Log("SlightlyDamged");
            foreach (var sprite in sprites)
            { 
                sprite.color = slightlyDamaged;
            }   
        }
        if (health.GetCurrentHealth() / health.maxHealth <= 0.50f)
        {
            foreach (var sprite in sprites)
            {
                sprite.color = damaged;
            }
        }
        if (health.GetCurrentHealth() / health.maxHealth <= 0.30f)
        {
            foreach (var sprite in sprites)
            {
                sprite.color = almostDead;
            }
        }
    }
}
