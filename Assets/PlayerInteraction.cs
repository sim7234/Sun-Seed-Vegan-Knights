using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer; 
    [SerializeField]
    private float interactRange = 1.5f; 
    [SerializeField]
    private Transform interactionPoint; 

    private DialogueInteract dialogueInteract;

    private PlayerInputActions playerControls;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerControls.ControlActions1.Interact.performed += OnInteract;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.ControlActions1.Interact.performed -= OnInteract;
        playerControls.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(interactionPoint.position, interactRange, interactableLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Interact")) 
            {
                dialogueInteract = hit.GetComponent<DialogueInteract>();
                if (dialogueInteract != null)
                {
                    dialogueInteract.Interact(); 
                }
            }
        }
    }
}