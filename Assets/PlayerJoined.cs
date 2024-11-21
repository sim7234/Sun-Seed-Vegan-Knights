using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoined : MonoBehaviour
{

public void OnPlayerJoined(PlayerInput playerInput)
{
    Debug.Log("OnPlayerJoined called.");
    if (playerInput.currentControlScheme == "Keyboard")
    {
        playerInput.SwitchCurrentActionMap("KeyboardActions1");
    }
    else if (playerInput.currentControlScheme == "Control")
    {
        playerInput.SwitchCurrentActionMap("ControlActions1");
    }

    Debug.Log($"Player {playerInput.playerIndex} joined with {playerInput.currentControlScheme}.");
}
}
