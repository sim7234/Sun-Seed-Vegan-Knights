using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnAttack : MonoBehaviour
{

    [SerializeField]
    private GameObject rotationObject;

    [SerializeField]
    private Rigidbody2D rb;

    [HideInInspector]
    public float strengthModifer = 1.0f;

    [SerializeField]
    private bool stopMovement;

    [SerializeField]
    private PlayerMovement playerMovement;
    private float baseMoveSpeed;



    [SerializeField]
    private float stopTime = 0.2f;
    private void Start()
    {
        if(playerMovement != null)
        {
            baseMoveSpeed = playerMovement.moveSpeed;
        }
    }
    public void moveWhenAttacking(float strength)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(rotationObject.transform.up * strength * strengthModifer, ForceMode2D.Impulse);
        strength = 1.0f;

        if(stopMovement && playerMovement != null)
        {
            StartCoroutine(stopMoveControlls());
        }
    }

    private IEnumerator stopMoveControlls()
    {
        playerMovement.moveSpeed = 10;
        playerMovement.rotationSpeed = 0;
        yield return new WaitForSeconds(stopTime);
        playerMovement.rotationSpeed = 1;
        playerMovement.moveSpeed = baseMoveSpeed;
    }
}