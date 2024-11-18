using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacks : MonoBehaviour
{
    Pathfinding pathfindingScript;

    Vector3 targetPosition;
    float distenceToTarget;

    public float distanceToAttack;
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
        if (!(pathfindingScript.target.Count <= 0))
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

        if (distenceToTarget > distanceToAttack && isAttacking == false)
        {
            pathfindingScript.followTarget = true;
            withinDistance = false;
        }
    }
}
