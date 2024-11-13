using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public PlayerInputActions playerMovement;
    public InputAction move;

    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerMovement = new PlayerInputActions();
    }

    private void OnEnable() // Correct capitalization
    {
        move = playerMovement.Player.Move;
        move.Enable();
    }

    private void OnDisable() // Correct capitalization
    {
        move.Disable();
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        Debug.Log(moveDirection); // Check if input is received
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
}