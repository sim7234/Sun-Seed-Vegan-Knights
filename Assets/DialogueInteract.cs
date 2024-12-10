using UnityEngine;
using TMPro;

public class DialogueInteract : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueUI;

    [SerializeField]
    private TMP_Text dialogueText; 

    [SerializeField]
    private string[] dialogueLines; 

    [SerializeField]
    private GameObject interactionButton;

    private int currentLine = 0; 
    private bool isDialogueActive = false; 

    private void Start()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false); 
        }
    }

    public void Interact()
    {
        if (!isDialogueActive)
        {
            StartDialogue();
        }
        else
        {
            ShowLine();
        }
    }

    public void StartDialogue()
    {
        if (dialogueUI != null && dialogueLines.Length > 0)
        {
            isDialogueActive = true; 
            dialogueUI.SetActive(true); 
            ShowLine(); 
        }
    }

    public void ShowLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine]; 
            currentLine++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);
        }
        isDialogueActive = false; 
        currentLine = 0; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactionButton != null && collision.CompareTag("Player"))
        {
            interactionButton.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactionButton != null && collision.CompareTag("Player"))
        {
            interactionButton.SetActive(false); 
        }
    }
}