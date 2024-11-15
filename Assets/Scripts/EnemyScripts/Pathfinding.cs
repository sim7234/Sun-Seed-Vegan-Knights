using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public List<GameObject> target = new List<GameObject>();

    NavMeshAgent agent;

    int totalTargets;

    int finalTarget;

  

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        ////To dynamically update what targets are available
        //totalTargets = 0;
        //foreach (GameObject t in target)
        //{
        //    totalTargets++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
        finalTarget = FindClosestTarget(totalTargets);
        if(target.Count > 0)
        {
            
            agent.SetDestination(target[finalTarget].transform.position);

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