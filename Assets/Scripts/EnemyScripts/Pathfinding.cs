using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [HideInInspector] public List<GameObject> target = new List<GameObject>();

    NavMeshAgent agent;

    [HideInInspector] public int totalTargets;

    [HideInInspector] public int finalTarget;

    [HideInInspector] public bool followTarget = true;

    [HideInInspector] public bool trackTarget = true;

    [HideInInspector] public Vector3 targetTransform;

    private void Start()
    {
        followTarget = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    public void Update()
    {
        finalTarget = FindClosestTarget(totalTargets);
        
        if (target.Count > 0)
        {
            if (target[finalTarget].gameObject == null)
                return;
            targetTransform = (target[finalTarget].transform.position);

            if (followTarget)
            {

                if (trackTarget == true)
                {
                    agent.SetDestination(target[finalTarget].transform.position);

                }
            }
        }
    }


    public int FindClosestTarget(int totalTargets)
    {
        float closestTarget = Mathf.Infinity;

        for (int i = 0; i < totalTargets; i++)
        {
            if (target[i].gameObject != null)
            {
                
                Vector3 targetDistence = target[i].transform.position - transform.position;
                float targetDistenceSquared = targetDistence.sqrMagnitude;

                if (targetDistenceSquared < closestTarget && target[i].gameObject.activeSelf == true)
                {
                    closestTarget = targetDistenceSquared;
                    
                    finalTarget = i;
                }
            }

        }
        return finalTarget;
    }
}