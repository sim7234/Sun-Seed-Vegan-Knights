using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerSpawnLocation : MonoBehaviour
{
    isTarget[] targets = new isTarget[3];

    private List<GameObject> players = new List<GameObject>();

    private void Awake()
    {
        targets = FindObjectsOfType<isTarget>();

        foreach (isTarget target in targets)
        {
            if (target.gameObject != null)
            {
                if(target.isActiveAndEnabled)
                {
                    players.Add(target.gameObject);
                }
            }
        }
    }
    void Start()
    {
        foreach (GameObject players in players)
        {
            players.transform.position = transform.position;
        }
    }

}
