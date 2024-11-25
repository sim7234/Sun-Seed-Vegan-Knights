// Ignore Spelling: Cooldown

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

    PlayerWater waterScript;
    SpriteRenderer sprite;

    private InputAction dashAction;

    Collider2D playerCollider;

    [SerializeField] GameObject rotationPoint;
    [SerializeField] TrailRenderer dashTrail;
    bool dashEffectEnabled;

   // float r2Value;

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
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        waterScript = GetComponent<PlayerWater>();
    }
    private void Update()
    {
        dashCooldownTimer -= Time.deltaTime;
    }

    private IEnumerator SpendWaterEffect()
    {
        playerCollider.enabled = false;
        dashTrail.emitting = true;
        sprite.color = Color.blue;

        yield return new WaitForSeconds(0.2f);

        sprite.color = Color.white;
        playerCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);

        dashTrail.emitting = false;
    }

    /*private void Update()
    {
        r2Value = Gamepad.current.rightTrigger.ReadValue();
        if (r2Value > 0)
        {
            r2Value = 1;
        }
    }*/

    public void Dash(InputAction.CallbackContext context)
    {
       if (context.phase != InputActionPhase.Started)
            return; 

       
        if (waterScript.TotalWater() >= 1 && dashCooldownTimer < 0)
        {
            //waterScript.TakeWater(1);
            dashCooldownTimer = dashCooldown;
            //rb.AddForce(-rotationPoint.transform.up * dashPower, ForceMode2D.Impulse);
            rb.AddForce(rb.velocity.normalized * dashPower, ForceMode2D.Impulse);
            StartCoroutine(SpendWaterEffect());
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
