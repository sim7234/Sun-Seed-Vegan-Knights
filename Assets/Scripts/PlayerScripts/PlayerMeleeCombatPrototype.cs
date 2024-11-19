using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeCombatPrototype : MonoBehaviour
{
    private bool inputR1 = false;
    private bool doesDamage = false;
    private Animator animator;
    public int swordDamage;

    private PlayerInput playerInput;
    private InputAction fireAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        fireAction.started += OnFireStarted;
        fireAction.canceled += OnFireCanceled;
    }

    private void OnDestroy()
    {
        fireAction.started -= OnFireStarted;
        fireAction.canceled -= OnFireCanceled;
    }

    private void Update()
    {
        if (inputR1)
        {
            doesDamage = true;
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                doesDamage = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();

            if (enemyHealth != null && doesDamage)
            {
                enemyHealth.TakeDamage(swordDamage);
                Debug.Log("Taken damage");
            }
            else
            {
                Debug.Log("Player did not take damage");
            }
        }
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        animator.SetTrigger("PressedR1");
        inputR1 = true;
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        inputR1 = false;
    }
}