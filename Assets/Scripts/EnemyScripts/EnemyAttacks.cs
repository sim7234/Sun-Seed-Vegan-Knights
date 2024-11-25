using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacks : MonoBehaviour
{
    Pathfinding pathfindingScript;

    protected Vector3 targetPosition;
    float distenceToTarget;
    //Hide to reduce compile time
    [HideInInspector] public float distanceToAttack = 20;

    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool withinDistance = false;

    NavMeshAgent agent;

 

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfindingScript = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        if (!(pathfindingScript.target.Count <= 0) && pathfindingScript.target[pathfindingScript.finalTarget] != null)
        {
            targetPosition = pathfindingScript.target[pathfindingScript.finalTarget].transform.position - transform.position;
        }
        
     
        distenceToTarget = targetPosition.sqrMagnitude;

        if (distenceToTarget < distanceToAttack)
        {
            withinDistance = true;
            pathfindingScript.followTarget = false;
            agent.velocity = Vector3.zero;
        }

        if (distenceToTarget > distanceToAttack)
        {
            pathfindingScript.followTarget = true;
            withinDistance = false;
        }
    }
}
