using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{

    private void Start()
    {
        InvokeRepeating(nameof(ColliderUpdate), 1, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Seed>() != null)
        {
            collision.GetComponent<Seed>().inSun = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Seed>() != null)
        {
            collision.GetComponent<Seed>().inSun = false;
        }
    }

    private void ColliderUpdate()
    {
        transform.Rotate(0, 0, 1, Space.Self);
        transform.Rotate(0, 0, -1, Space.Self);
    }
}
