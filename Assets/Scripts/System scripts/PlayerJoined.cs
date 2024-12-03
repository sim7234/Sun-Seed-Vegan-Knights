using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoined : MonoBehaviour
{
    public Dialogue dialogueSystem; 

    private PlayerInputManager playerInputManager;

    void OnEnable()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined += OnPlayerJoined;
        }
    }

    void OnDisable()
    {
        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoined;
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        DontDestroyOnLoad(playerInput.gameObject);

        if (playerInput.currentControlScheme == "Control" || playerInput.currentControlScheme == "Keyboard")
        {
            playerInput.SwitchCurrentActionMap("ControlActions1");
        }

        if (dialogueSystem != null)
        {
            dialogueSystem.OnPlayerJoined(playerInput);

            playerInput.actions["NextDialogue"].performed += context =>
            {
                if (dialogueSystem.IsDialogueActive) 
                {
                    dialogueSystem.NextLine();
                }
            };

            playerInput.actions["PreviousDialogue"].performed += context =>
            {
                if (dialogueSystem.IsDialogueActive)
                {
                    dialogueSystem.PreviousLine();
                }
            };
        }
    }
}