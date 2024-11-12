using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float maxSpeed = 4;

    private Rigidbody2D rb2d;

    [SerializeField]
    private GameObject directionIndicator;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 vecloity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        vecloity.Normalize();
        if (vecloity.magnitude > 0 && rb2d.velocity.magnitude <= maxSpeed)
        {
            rb2d.velocity += vecloity * moveSpeed;
        }

        Vector3 direction = rb2d.velocity.normalized;
        directionIndicator.transform.up = direction;
    }
}
