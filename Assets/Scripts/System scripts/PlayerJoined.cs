using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerJoined : MonoBehaviour
{
    public Dialogue dialogueSystem; 
    public TextMeshProUGUI messageText;
    private PlayerInputManager playerInputManager;
    public CameraMoverOnEnemyDeath cameraMover;


     private void Start()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true); 
        }
    }

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

        if (messageText != null)
        {
            messageText.text = "Use   <voffset=0.3em><sprite=3></voffset>to move and   <voffset=0.3em><sprite=0></voffset>to rotate"; 
            Invoke(nameof(HideMessage), 10f); 
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
    private void HideMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
         if (cameraMover != null)
        {
            StartCoroutine(StartDialogueCoroutine());
        }
    }

    private IEnumerator StartDialogueCoroutine()
    {
        yield return cameraMover.ShowDialogueAfterDelay(0f);
    }
}
