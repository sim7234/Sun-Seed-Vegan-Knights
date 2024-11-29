using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        if (playerInputManager != null)
        {
            playerInputManager.onPlayerJoined -= OnPlayerJoined;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        DontDestroyOnLoad(playerInput.gameObject);

        if (playerInput.currentControlScheme == "Control")
        {
            playerInput.SwitchCurrentActionMap("ControlActions1");
        }
        else if (playerInput.currentControlScheme == "Keyboard")
        {
            playerInput.SwitchCurrentActionMap("ControlActions1");
        }

        if (dialogueSystem != null)
        {
            playerInput.actions["NextDialogue"].performed += context => dialogueSystem.NextLine();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TeodorHub" || scene.name == "Menu")
        {
            ResetPlayerInputManager();
        }
    }

    private void ResetPlayerInputManager()
    {
        PlayerInput[] allPlayers = FindObjectsOfType<PlayerInput>();
        foreach (var player in allPlayers)
        {
            Destroy(player.gameObject);
        }

        if (playerInputManager != null)
        {
            Destroy(playerInputManager.gameObject);
        }

        SceneManager.sceneLoaded -= OnSceneLoaded; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}