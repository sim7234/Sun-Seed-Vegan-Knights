using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private Collider2D damageCollider;

    [SerializeField]
    private bool isTriggerd;

    private void Start()
    {
        damageCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isTriggerd)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggerd)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    public void TurnOnCollider()
    {
        damageCollider.enabled = true;
    }
    public void TurnOfCollider()
    {
        damageCollider.enabled = false;
    }
}