using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI progressText; 
    [TextArea(3, 10)]
    public string[] lines;

    private int index;
    private PlayerInput playerInput;

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
        Debug.Log("Pressed next");
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
            Debug.LogError("Dialogue lines are empty or null.");
            return;
        }

        lines = newLines;
        index = 0;
        IsDialogueActive = true; 
        gameObject.SetActive(true);
        UpdateProgress(); 
        DisplayLine();

        DisableActionMap();
    }

    private void DisplayLine()
    {
        if (index >= 0 && index < lines.Length)
        {
            textComponent.text = lines[index];
            UpdateProgress(); 
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

    private void UpdateProgress()
    {
        progressText.text = $"{index + 1}/{lines.Length}";
    }

    private void EndDialogue()
    {
        IsDialogueActive = false; 
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
        progressText.text = string.Empty; 

        EnableActionMap();
    }

    private void DisableActionMap()
    {
        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap("UI"); 
        }
    }

    private void EnableActionMap()
    {
        if (playerInput != null)
        {
            playerInput.SwitchCurrentActionMap(actionMapToDisable); 
        }
    }
}