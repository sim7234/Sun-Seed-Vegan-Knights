using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;   
    [TextArea(3, 10)]
    public string[] lines; 
    
    public float textSpeed; 
    private int index; 
    private PlayerInput playerInput;
    
    private bool isTyping = false;

    void Start()
    {
        textComponent.text = string.Empty; 
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
        textComponent.text = string.Empty; 
        gameObject.SetActive(true); 
        StartCoroutine(TypeLine()); 
    }

    private IEnumerator TypeLine()
    {
        isTyping = true; 
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c; 
            yield return new WaitForSeconds(textSpeed); 
        }
        isTyping = false; 
    }

    public void NextLine()
    {
        if (isTyping) 
        {
            StopAllCoroutines();
            textComponent.text = lines[index]; 
            isTyping = false; 
        }
        else if (index < lines.Length - 1) 
        {
            index++;
            textComponent.text = string.Empty; 
            StartCoroutine(TypeLine()); 
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