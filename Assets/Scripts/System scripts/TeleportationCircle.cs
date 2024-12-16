using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportationCircle : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber = 0;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke(nameof(Teleport), 1);
    }
    private void Teleport()
    {
        if(sceneNumber != 0)
        {
            SceneManager.LoadScene(sceneNumber);
        }
        else
        {
            SceneManager.LoadScene(SaveData.Instance.completedMission + 1);
        }
    }
}
