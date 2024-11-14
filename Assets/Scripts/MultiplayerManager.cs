using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab; // Prefab for Player 1 with WASD controls
    [SerializeField] private GameObject player2Prefab; // Prefab for Player 2 with Arrow keys

    private int playerCount = 0;

    private void Update()
    {
        // Spawn Player 1 on Space key press
        if (playerCount == 0 && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnPlayer(player1Prefab, "Player1Keyboard", "Player1Actions", "Player 1");
        }
        // Spawn Player 2 on Enter key press
        else if (playerCount == 1 && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SpawnPlayer(player2Prefab, "Player2Keyboard", "Player2Actions", "Player 2");
        }
    }

    private void SpawnPlayer(GameObject prefab, string controlScheme, string actionMap, string playerName)
    {
        // Instantiate the player prefab with the specific control scheme and action map
        var playerInput = PlayerInput.Instantiate(prefab,
            controlScheme: controlScheme, 
            pairWithDevices: new InputDevice[] { Keyboard.current });

        playerInput.SwitchCurrentActionMap(actionMap); // Set the specific action map
        playerInput.gameObject.name = playerName;
        Debug.Log($"{playerName} joined with {controlScheme} scheme and {actionMap} action map.");

        playerCount++;
    }
}