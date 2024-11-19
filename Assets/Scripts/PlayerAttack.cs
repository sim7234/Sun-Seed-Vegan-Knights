using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;

    private Collider2D weaponCollider;
    private Animator weaponAnimator;

    private PlayerInput playerInput;
    private InputAction fireAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            fireAction = playerInput.actions["Fire"];
        }
        else
        {
            Debug.LogError("PlayerInput component is missing on this GameObject.");
        }
    }

    private void Start()
    {
        weaponCollider = weapon.GetComponent<Collider2D>();
        weaponAnimator = weapon.GetComponent<Animator>();

        if (fireAction != null)
        {
            fireAction.performed += OnFirePerformed;
        }
        else
        {
            Debug.LogError("Fire action could not be found. Check the Input Action Asset.");
        }
    }

    private void OnDestroy()
    {
        if (fireAction != null)
        {
            fireAction.performed -= OnFirePerformed;
        }
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        weaponAnimator.SetTrigger("PressedR1");
    }
}