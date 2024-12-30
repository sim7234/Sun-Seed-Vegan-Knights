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
        if (playerInput.devices.Count > 0 && 
            (playerInput.devices[0] is Keyboard || playerInput.devices[0] is Mouse))
        {
            Destroy(playerInput.gameObject);
            return;
        }

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

        InputAction pauseAction = playerInput.actions["Pause"];
        if (pauseAction != null)
        {
            pauseAction.Disable();
            StartCoroutine(ReenablePauseAction(pauseAction));
        }

        PlayerAttack playerAttack = playerInput.GetComponent<PlayerAttack>();
        if (playerAttack == null)
        {
            playerAttack = playerInput.gameObject.AddComponent<PlayerAttack>();
        }
        playerAttack.Initialize(playerInput);

        if (messageText != null)
        {
            messageText.text = "Use   <voffset=0.3em><sprite=3></voffset>to move and   <voffset=0.3em><sprite=0></voffset>to rotate";
            Invoke(nameof(HideMessage), 5f);
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

    private IEnumerator ReenablePauseAction(InputAction pauseAction)
    {
        yield return null;
        pauseAction.Enable();
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
        yield return cameraMover.ShowDialogueAfterDelay(3.5f);
    }

    private IEnumerator ShowSecondMessageCoroutine()
    {
        yield return new WaitForSeconds(0f);
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "Press   <voffset=0.3em><sprite=2></voffset>to dash.";
            Invoke(nameof(HideSecondMessage), 3.5f);
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