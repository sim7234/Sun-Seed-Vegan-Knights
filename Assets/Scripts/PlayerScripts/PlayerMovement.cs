using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float maxSpeed = 4;

    private Rigidbody2D rb2d;

    [SerializeField]
    private GameObject directionIndicator;
    
    public PlayerInputActions playerMovement;
    public InputAction move;

    Vector2 moveDirection = Vector2.zero;


    private void Awake()
    {
         playerMovement = new PlayerInputActions();
    }
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

     public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    private void OnEnable() 
    {
        move = playerMovement.Player.Move;
        move.Enable();
    }

    private void OnDisable() 
    {
        move.Disable();
    }

    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            directionIndicator.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
        private void FixedUpdate()
        {
            rb2d.velocity = moveDirection * moveSpeed;
        }
    }