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

    private PlayerInput firstPlayerInput; 

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

        if (firstPlayerInput == null)
        {
            firstPlayerInput = playerInput;
            playerInput.SwitchCurrentActionMap("ControlActions1"); 
        }
        else
        {
            playerInput.SwitchCurrentActionMap("ControlActions1");
        }

        if (messageText != null)
        {
            messageText.text = "Use   <voffset=0.3em><sprite=3></voffset>to move and   <voffset=0.3em><sprite=0></voffset>to aim";
            Invoke(nameof(HideMessage), 10f);
        }

        if (dialogueSystem != null && playerInput == firstPlayerInput) 
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
        StartCoroutine(ShowSecondMessageCoroutine());
    }

    private IEnumerator StartDialogueCoroutine()
    {
        yield return cameraMover.ShowDialogueAfterDelay(5f);
    }

    private IEnumerator ShowSecondMessageCoroutine()
    {
        yield return new WaitForSeconds(0f);
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "Press   <voffset=0.3em><sprite=2></voffset>to dash.";
            Invoke(nameof(HideSecondMessage), 5f);
        }
    }

    private void HideSecondMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }
}