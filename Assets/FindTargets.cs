using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FindTargets : MonoBehaviour
{
    [SerializeField]
    private Pathfinding pathfinding;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Found Player");
            pathfinding.target.Add(other.gameObject);
        }

        if (other.CompareTag("Objective"))
        {
            Debug.Log("Found Objective");
            pathfinding.target.Add(other.gameObject);
        }
    }

}
