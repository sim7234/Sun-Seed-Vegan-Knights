using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashPower;

    [SerializeField]
    private float dashCooldown;
    private float dashCooldownTimer;

    Rigidbody2D rb;

    private InputAction dashAction;

    Collider2D playerCollider;

    [SerializeField] GameObject rotationPoint;
    [SerializeField] TrailRenderer dashTrail;
    [SerializeField] float raycastDistance = 1f; 
    [SerializeField] LayerMask collisionMask; 

    bool dashEffectEnabled;

    [SerializeField]
    private AudioClip dashSound;
    private AudioSource audioSource;

    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            dashAction = playerInput.actions["Dash"];
        }
    }
    
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        dashTrail.emitting = false;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        dashCooldownTimer -= Time.deltaTime;
    }

    private IEnumerator SpendWaterEffect()
    {
        playerCollider.enabled = false;
        dashTrail.emitting = true;
        

        Vector2 dashDirection = rb.velocity.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, raycastDistance, collisionMask);

        if (hit.collider != null)
        {
            playerCollider.enabled = true;
            yield break;
        }

        yield return new WaitForSeconds(0.2f);

        GetComponent<LookOnSystem>().AttackClosestEnemy();
        playerCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);

        dashTrail.emitting = false;
    }


    public void Dash(InputAction.CallbackContext context)
    {
       if (context.phase != InputActionPhase.Started)
            return; 

        if (dashCooldownTimer < 0)
        {
            dashCooldownTimer = dashCooldown;
            rb.AddForce(rb.velocity.normalized * dashPower, ForceMode2D.Impulse);
            StartCoroutine(SpendWaterEffect());
            audioSource.PlayOneShot(dashSound);
        }
    }

    private void OnEnable()
    {
        if (dashAction != null)
        {
            dashAction.performed += Dash;
            dashAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (dashAction != null)
        {
            dashAction.performed -= Dash;
            dashAction.Disable();
        }
    }
}
