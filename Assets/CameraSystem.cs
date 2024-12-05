using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private int playerCount;

    private isTarget[] targets = new isTarget[3];

    [SerializeField]
    private List<GameObject> players = new List<GameObject>();

    private void Start()
    {
        FindTargets();
    }
    private void Update()
    {
        if (SaveData.Instance.playerAmount != playerCount)
        {
            playerCount = SaveData.Instance.playerAmount;
            FindTargets();
        }



    }
    
    private void FindTargets()
    {
        targets = FindObjectsOfType<isTarget>();

        foreach (isTarget target in targets)
        {
            if (target.gameObject != null)
            {
                players.Add(target.gameObject);
            }
        }
    }
}
