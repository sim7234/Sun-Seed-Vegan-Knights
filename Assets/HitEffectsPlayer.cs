using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectsPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject onHitEffect;

    [SerializeField]
    private List<SpriteRenderer> allPlayerSprites = new List<SpriteRenderer>();

    [SerializeField]
    private float blinkTime = 0.1f;

    private Color baseColor;

    private float effectcooldown;

    private void Start()
    {
        baseColor = allPlayerSprites[0].color;
    }
    private void Update()
    {
        effectcooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Damage>() != null && collision.gameObject.CompareTag("Enemy"))
        {
            if (effectcooldown <= 0)
            {
                effectcooldown = 2;
                foreach (var sprite in allPlayerSprites)
                {
                    sprite.color = Color.red;
                }
                Invoke(nameof(ResetColor), blinkTime);

                Vector2 direction = new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y);

                GameObject newHitEffect = Instantiate(onHitEffect, transform.position, Quaternion.identity);
                newHitEffect.transform.up = -direction;
                Destroy(newHitEffect, 2f);

            }
        }
    }

    private void ResetColor()
    {
        foreach (var sprite in allPlayerSprites)
        {
            sprite.color = baseColor;
        }
    }
}
