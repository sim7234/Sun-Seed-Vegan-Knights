using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private float knockbackStrength;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * knockbackStrength, ForceMode2D.Impulse);
        }
    }
}
