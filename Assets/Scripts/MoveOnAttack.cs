using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject rotationObject;

    [SerializeField]
    private Rigidbody2D rb;

    public void moveWhenAttacking(float strength)
    {
        rb.AddForce(rotationObject.transform.up * strength, ForceMode2D.Impulse);
    }
}
