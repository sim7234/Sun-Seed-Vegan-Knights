using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public List<GameObject> target = new List<GameObject>();

    NavMeshAgent agent;

    int totalTargets;
    int finalTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            target.Add(other.gameObject);
        }

        if (other.CompareTag("Objective"))
        {
            Debug.Log("Found Objective");
            target.Add(other.gameObject);
        }
    }

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        
        getTotalTargets();
    }

    // Update is called once per frame
    void Update()
    {

        
        finalTarget = FindClosestTarget(totalTargets);

        agent.SetDestination(target[finalTarget].transform.position); 
    }


    void getTotalTargets()
    {
        totalTargets = 0;
        foreach (GameObject t in target)
        {
            totalTargets++;
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
