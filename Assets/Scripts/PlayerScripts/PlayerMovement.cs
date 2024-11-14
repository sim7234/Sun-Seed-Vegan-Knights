using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4f;

    [SerializeField]
    private float maxSpeed = 4f;

    private Rigidbody2D rb2d;

    [SerializeField]
    private GameObject directionIndicator;

    private Vector2 moveDirection = Vector2.zero;
    private InputAction move;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Get the PlayerInput component attached to this player instance
        var playerInput = GetComponent<PlayerInput>();
        
        // Ensure the player input component exists and get the "Move" action from the current action map
        if (playerInput != null)
        {
            move = playerInput.actions["Move"];
            move.Enable();
            move.performed += OnMove;
            move.canceled += OnMove;
        }
        else
        {
            Debug.LogError("PlayerInput component not found on " + gameObject.name);
        }
    }

    private void OnDisable()
    {
        if (move != null)
        {
            move.Disable();
            move.performed -= OnMove;
            move.canceled -= OnMove;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Read the movement input vector
        moveDirection = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            directionIndicator.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    private void FixedUpdate()
    {
        // Apply movement with clamped speed
        Vector2 velocity = moveDirection * moveSpeed;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        rb2d.velocity = velocity;
    }
}