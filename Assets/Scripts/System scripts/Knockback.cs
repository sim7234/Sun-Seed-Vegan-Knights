using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private float knockbackStrength;

    [SerializeField]
    private GameObject knockBackSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(knockBackSource != null)
        {
            if (collision.GetComponent<Rigidbody2D>() != null)
            {
                collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - knockBackSource.transform.position).normalized * knockbackStrength, ForceMode2D.Impulse);
            }
        }
        else
        {
            if(collision.GetComponent<Rigidbody2D>() != null)
            {
                collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * knockbackStrength, ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (knockBackSource != null)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - knockBackSource.transform.position).normalized * knockbackStrength, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * knockbackStrength, ForceMode2D.Impulse);
            }
        }
    }
}
