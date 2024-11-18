using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [HideInInspector] public List<GameObject> target = new List<GameObject>();

    NavMeshAgent agent;

    int totalTargets;

    [HideInInspector] public int finalTarget;

    [HideInInspector] public bool followTarget = true;

  

    private void Start()
    {
        followTarget = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        finalTarget = FindClosestTarget(totalTargets);
        if(target.Count > 0)
        {
            if (followTarget)
            {
                agent.SetDestination(target[finalTarget].transform.position);
            }
        }
    }


    int FindClosestTarget(int totalTargets)
    {
        float closestTarget = Mathf.Infinity;

        for (int i = 0; i < totalTargets; i++)
        {

            Vector3 targetDistence = target[i].transform.position - transform.position;
            float targetDistenceSquared = targetDistence.sqrMagnitude;

            if (targetDistenceSquared < closestTarget)
            {
                closestTarget = targetDistenceSquared;

                finalTarget = i;
            }

        }
        return finalTarget;
    }
}