using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashPower;

    Rigidbody2D rb;

    PlayerWater waterScript;
    SpriteRenderer sprite;

    private InputAction dashAction;

    Collider2D playerCollider;

    [SerializeField] GameObject rotationPoint;
    [SerializeField] TrailRenderer dashTrail;
    bool dashEffectEnabled;

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

    public void Dash(InputAction.CallbackContext context)
    {
        if (waterScript.TotalWater() >= 1)
        {
            waterScript.TakeWater(1);
            rb.AddForce(rotationPoint.transform.up * dashPower, ForceMode2D.Impulse);
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
