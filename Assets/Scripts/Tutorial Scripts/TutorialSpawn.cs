using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialSpawn : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints; 

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        int playerIndex = playerInput.playerIndex;

        if (playerIndex < spawnPoints.Count)
        {
            playerInput.transform.position = spawnPoints[playerIndex].position;
        }
    }
}