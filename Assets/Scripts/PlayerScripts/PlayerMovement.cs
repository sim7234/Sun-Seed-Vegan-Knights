using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4f;

    public AnimationCurve curve;

    [SerializeField]
    private float maxSpeed = 4f;

    public float runSpeedMultiplicative;
    bool isRunning;
    public float timeUntilRun;
    public  float lastAttackTime = 0;

    Vector2 velocity;

    private Rigidbody2D rb2d;

    [SerializeField]
    private GameObject directionIndicator;

    private Vector2 moveDirection = Vector2.zero;
    private InputAction move;

    private Vector2 rotationDirection = Vector2.zero;
    private InputAction rotate;

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

            rotate = playerInput.actions["Rotate"];
            rotate.Enable();
            rotate.performed += RotationDirection;
            rotate.canceled += RotationDirection;
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
        if (rotate != null)
        {
            rotate.Disable();
            rotate.performed -= RotationDirection;
            rotate.canceled -= RotationDirection;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        moveDirection = context.ReadValue<Vector2>();
    }

    private void RotationDirection(InputAction.CallbackContext context)
    {
        rotationDirection = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if(rotationDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
            directionIndicator.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        if (lastAttackTime >= 0)
        {
            lastAttackTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            lastAttackTime = timeUntilRun;
            isRunning = false;
        }

        if (lastAttackTime <= 0)
        {
            isRunning = true;
        }

    }

    private void FixedUpdate()
    {

        if (isRunning == true)
        {
            velocity = moveDirection.normalized * moveSpeed * runSpeedMultiplicative;
        }
        else
        {
            velocity = moveDirection.normalized * moveSpeed;
        }

        if (velocity.sqrMagnitude < (maxSpeed * maxSpeed))
        {
            rb2d.velocity += velocity;
        } 
    }
}