using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectsEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject onHitEffect;

    private Color baseColor;

    private float effectcooldown;

    private void Start()
    {

    }
    private void Update()
    {
        effectcooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Damage>() != null)
        {
            if (effectcooldown <= 0)
            {
                effectcooldown = 0.1f;

                Vector2 direction = new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y);

                GameObject newHitEffect = Instantiate(onHitEffect, transform.position, Quaternion.identity);
                newHitEffect.transform.up = -direction;
                Destroy(newHitEffect, 2f);
            }
        }
    }
}
