using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private Collider2D damageCollider;

    private void Start()
    {
        damageCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
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