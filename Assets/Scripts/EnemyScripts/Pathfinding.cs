using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public List<Transform> target;

    NavMeshAgent agent;

    int totalTargets;

    int finalTarget;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //To dynamically update what targets are available
        totalTargets = 0;
        foreach (Transform t in target)
        {
            totalTargets++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        finalTarget = FindClosestTarget(totalTargets);

        agent.SetDestination(target[finalTarget].position);
    }


    int FindClosestTarget(int totalTargets)
    {
        float closestTarget = Mathf.Infinity;

        for (int i = 0; i < totalTargets; i++)
        {


            {
                Vector3 targetDistence = target[i].position - transform.position;
                float targetDistenceSquared = targetDistence.sqrMagnitude;

                if (targetDistenceSquared < closestTarget)
                {
                    closestTarget = targetDistenceSquared;

                    finalTarget = i;
                }
            }
        }
        return finalTarget;
    }
}
