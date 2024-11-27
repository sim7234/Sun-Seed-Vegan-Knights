using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 4f;

    public AnimationCurve curve;

    [SerializeField]
    private float maxSpeed = 4f;

    public float runSpeedMultiplicative;
    bool isRunning;
    public float timeUntilRun;
    float lastAttackTime = 0;

    [SerializeField]
    private float deAcceleration = 0.96f;

    Vector2 accerleation;

    private Rigidbody2D rb2d;

    [SerializeField]
    public GameObject directionIndicator;

    private Vector2 moveDirection = Vector2.zero;
    private InputAction move;

    private Vector2 rotationDirection = Vector2.zero;
    private InputAction rotate;

    [SerializeField]
    private List<AudioClip> movementSounds;
    private AudioSource audioSource;
    private bool isMoving;

    //Save data stuff
    public int playerIndex;

    public bool lockOn;

    
    private void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        //Debug.Log($"Player {playerInput.playerIndex} has joined.");

        SaveData.Instance.playerAmount++;
        playerIndex = SaveData.Instance.playerAmount;
        SaveData.Instance.FixHud(gameObject, playerIndex);
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();

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

        if (moveDirection != Vector2.zero && !audioSource.isPlaying)
        {
            PlayRandomMovementSound();
        }
    }

    private void RotationDirection(InputAction.CallbackContext context)
    {
        rotationDirection = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if(rotationDirection != Vector2.zero && !lockOn)
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
        if (moveDirection != Vector2.zero) 
        {
            if (!isMoving) 
            {
                isMoving = true; 
                PlayRandomMovementSound(); 
            }

            else if (!audioSource.isPlaying) 
            {
                PlayRandomMovementSound(); 
            }
        }

        else 
        {
            if (isMoving) 
            {
                isMoving = false; 
                audioSource.Stop(); 
            }
        }
    }

    private void FixedUpdate()
    {

        accerleation = moveDirection.normalized * moveSpeed * Time.deltaTime;
        accerleation *= runSpeedMultiplicative;

        if((rb2d.velocity + accerleation).magnitude < maxSpeed)
        {
            rb2d.velocity += accerleation;
        }
        
        rb2d.velocity -= rb2d.velocity * deAcceleration * Time.deltaTime;
        
    }    
    private void PlayRandomMovementSound()
    {
        if (movementSounds.Count > 0)
        {
            int randomIndex = Random.Range(0, movementSounds.Count);
            audioSource.clip = movementSounds[randomIndex]; 
            audioSource.Play(); 
        }
    }
}