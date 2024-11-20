using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FindTargets : MonoBehaviour
{
    [SerializeField]
    private Pathfinding pathfinding;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Found Player");
    //        pathfinding.target.Add(other.gameObject);
    //        Invoke(nameof(TurnOfScanCollider), 0.1f);
    //    }

    //    if (other.CompareTag("Objective"))
    //    {
    //        Debug.Log("Found Objective");
    //        pathfinding.target.Add(other.gameObject);
    //    }
    //}

    private void Start()
    {
        InvokeRepeating(nameof(FindObjects), 2, 2);
    }

    private void FindObjects()
    {
        if (GameObject.FindWithTag("Player") != null)
        pathfinding.target.Add(GameObject.FindWithTag("Player"));
        if(GameObject.FindWithTag("Objective") != null)
        pathfinding.target.Add(GameObject.FindWithTag("Objective"));
    }
}
