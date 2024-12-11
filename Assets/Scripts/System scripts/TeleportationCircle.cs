using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportationCircle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke(nameof(Teleport), 3);
    }
    private void Teleport()
    {
        SceneManager.LoadScene(SaveData.Instance.completedMission + 1);
    }
}
