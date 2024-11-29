using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;   
    [TextArea(3, 10)]
    public string[] lines; 
    
    private int index; 
    private PlayerInput playerInput;

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
        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.actions["NextDialogue"].performed -= OnNextDialoguePerformed;
        }
    }

    private void OnNextDialoguePerformed(InputAction.CallbackContext context)
    {
        NextLine(); 
    }

    public void StartDialogue(string[] newLines)
    {
        lines = newLines; 
        index = 0; 
        gameObject.SetActive(true); 
        DisplayLine(); 
    }

    private void DisplayLine()
    {
        if (index >= 0 && index < lines.Length)
        {
            textComponent.text = lines[index];
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

    private void EndDialogue()
    {
        gameObject.SetActive(false); 
        textComponent.text = string.Empty; 
    }
}