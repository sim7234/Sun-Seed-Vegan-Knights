using UnityEngine;
using UnityEngine.AI;

public class EnemyRetreat : MonoBehaviour
{

    Pathfinding pathfindingScript;
    EnemyAttacks enemyAttackScript;
    NavMeshAgent agent;

    int currentTarget;

    public float baseSpeed;
    public float retreatSpeed;
    public float whenToRetreat;

    Vector3 retreatDestination;
    [HideInInspector] public bool retreating;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfindingScript = GetComponent<Pathfinding>();
        enemyAttackScript = GetComponent<EnemyAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        //retreat if enemy gets within distance
        currentTarget = pathfindingScript.FindClosestTarget(pathfindingScript.totalTargets);

        retreatDestination = -(pathfindingScript.target[currentTarget].transform.position - transform.position).normalized * whenToRetreat;
        if (enemyAttackScript.distenceToTarget < whenToRetreat)
        {
            pathfindingScript.trackTarget = false;
            retreat();
        }
        else
        {
            retreating = false;
            agent.speed = baseSpeed;
            pathfindingScript.trackTarget = true;
        }
    }
    void retreat()
    {
        retreating = true;
        agent.speed = retreatSpeed;
        agent.SetDestination(retreatDestination);
    }
}
