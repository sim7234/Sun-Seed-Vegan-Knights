using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab; // Prefab for Player 1 with WASD controls
    [SerializeField] private GameObject player2Prefab; // Prefab for Player 2 with Arrow keys

    private int playerCount = 0;

    private void Update()
    {
        if (playerCount == 0 && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnPlayer(player1Prefab, "Player1Keyboard", "Player1Actions", "Player 1", Keyboard.current);
        }
        else if (playerCount == 1 && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SpawnPlayer(player2Prefab, "Player2Keyboard", "Player2Actions", "Player 2", Keyboard.current);
        }
        else if (playerCount == 0 && Gamepad.current != null && Gamepad.current.dpad.up.wasPressedThisFrame) 
        {
            SpawnPlayer(player1Prefab, "PlayerControllerInput1", "Player3Actions", "Player 1", Gamepad.current);
        }
        else if (playerCount == 1 && Gamepad.current != null && Gamepad.current.dpad.down.wasPressedThisFrame) 
        {
            SpawnPlayer(player2Prefab, "PlayerControllerInput1", "Player3Actions", "Player 2", Gamepad.current);
        }
    }

    private void SpawnPlayer(GameObject prefab, string controlScheme, string actionMap, string playerName, InputDevice device)
    {
        var playerInput = PlayerInput.Instantiate(prefab,
            controlScheme: controlScheme, 
            pairWithDevices: new InputDevice[] { device });

        playerInput.SwitchCurrentActionMap(actionMap); 
        playerInput.gameObject.name = playerName;
        Debug.Log($"{playerName} joined with {controlScheme} scheme and {actionMap} action map.");

        playerCount++;
    }
}