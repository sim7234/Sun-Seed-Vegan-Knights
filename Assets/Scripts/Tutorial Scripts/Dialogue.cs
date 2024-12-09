using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea(3, 10)]
    public string[] lines;

    private int index;
    private PlayerInput playerInput;
    public UnityEvent<int> onDialogueLineChanged;
    
    public string actionMapToDisable = "ControlActions1"; 
    public bool IsDialogueActive { get; private set; } 

    private void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue(lines);
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (this.playerInput == null)
        {
            this.playerInput = playerInput;

            playerInput.actions["NextDialogue"].performed += OnNextDialoguePerformed;
            playerInput.actions["PreviousDialogue"].performed += OnPreviousDialoguePerformed; 
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.actions["NextDialogue"].performed -= OnNextDialoguePerformed;
            playerInput.actions["PreviousDialogue"].performed -= OnPreviousDialoguePerformed;
        }
    }

    private void OnEnable()
    {
        if (playerInput != null)
        {
            playerInput.actions["NextDialogue"].Enable();
            playerInput.actions["PreviousDialogue"].Enable();
        }
    }

    private void OnNextDialoguePerformed(InputAction.CallbackContext context)
    {
        NextLine();
    }

    private void OnPreviousDialoguePerformed(InputAction.CallbackContext context)
    {
        PreviousLine();
    }

    public void StartDialogue(string[] newLines)
    {
        if (newLines == null || newLines.Length == 0)
        {
            return;
        }

        lines = newLines;
        index = 0;
        IsDialogueActive = true; 
        gameObject.SetActive(true); 
        DisplayLine();

        DisableActionMap();
    }

    private void DisplayLine()
    {
        if (index >= 0 && index < lines.Length)
        {
            textComponent.text = lines[index];
            onDialogueLineChanged?.Invoke(index); 
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            DisplayLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public void PreviousLine()
    {
        if (index > 0)
        {
            index--;
            DisplayLine();
        }
    }

    private void EndDialogue()
    {
        IsDialogueActive = false; 
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
        EnableActionMap();
    }

    private PlayerInput dialogueControllerPlayer; 

    private void DisableActionMap()
    {
        var players = FindObjectsOfType<PlayerInput>();

        foreach (PlayerInput player in players)
        {
            if (player == dialogueControllerPlayer)
            {
                player.SwitchCurrentActionMap("UI"); 
            }
            else
            {
                player.SwitchCurrentActionMap("Disabled"); 
            }
        }
    }

    private void EnableActionMap()
    {
        foreach (PlayerInput player in FindObjectsOfType<PlayerInput>())
        {
            if (player == dialogueControllerPlayer)
            {
                player.SwitchCurrentActionMap(actionMapToDisable); 
            }
            else
            {
                player.SwitchCurrentActionMap("ControlActions1"); 
            }
        }
    }
}