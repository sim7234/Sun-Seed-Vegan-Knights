using UnityEngine;

public class KillPlayersOnStart : MonoBehaviour
{
    void Awake()
    {
        SaveData.Instance.playerAmount = 0;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        CameraSystem cameraScript = FindAnyObjectByType<CameraSystem>();

        if (cameraScript != null)
        {
            cameraScript.playerCount = 0;
            cameraScript.FindTargets();
        }

        foreach (GameObject p in players)
        {
            cameraScript.players.Remove(p);
            Destroy(p);
        }
    }
}
