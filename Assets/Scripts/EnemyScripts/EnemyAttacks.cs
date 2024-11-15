using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacks : MonoBehaviour
{

    Pathfinding pathfindingScript;

    Vector3 targetPosition;
    float distenceToTarget;

    public float distenceToAttack;
    bool attacking = false;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfindingScript = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        targetPosition = pathfindingScript.target[pathfindingScript.finalTarget].transform.position - transform.position;

        
        distenceToTarget = targetPosition.sqrMagnitude;

        if (distenceToTarget < distenceToAttack)
        {
            pathfindingScript.followTarget = false;
            agent.velocity = Vector3.zero;
            Debug.Log("Within Distance");
        }

        if (distenceToTarget > distenceToAttack && attacking == false)
        {
            pathfindingScript.followTarget = true;
        }


    }


}
