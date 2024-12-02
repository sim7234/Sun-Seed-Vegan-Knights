using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReposition : MonoBehaviour
{
    public Transform[] spawnPoints;

    void Start()
    {
        PlayerInput[] players = FindObjectsOfType<PlayerInput>();

        for (int i = 0; i < players.Length; i++)
        {
            if (i < spawnPoints.Length)
            {
                players[i].transform.position = spawnPoints[i].position;
            }
        }
    }
}