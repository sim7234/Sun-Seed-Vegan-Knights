using UnityEngine;
using UnityEngine.AI;

public class EnemyRangedAttack : MonoBehaviour
{

    Pathfinding pathfindingScript;
    EnemyAttacks enemyAttackScript;
    [SerializeField] GameObject retreatPosition;
    NavMeshAgent agent;

    public float whenToRetreat;
    Rigidbody2D rb;

    int currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        pathfindingScript = GetComponent<Pathfinding>();
        enemyAttackScript = GetComponent<EnemyAttacks>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        retreatPosition.transform.position = (transform.position - pathfindingScript.target[currentTarget].transform.position);
        //retreat if enemy gets within distance

        currentTarget = pathfindingScript.FindClosestTarget(pathfindingScript.totalTargets);

        if (enemyAttackScript.distenceToTarget < whenToRetreat)
        {
            pathfindingScript.trackTarget = false;
            retreat();
        }
        else
        {
            pathfindingScript.trackTarget = true;
        }

    }

    void retreat()
    {
        agent.SetDestination(-(pathfindingScript.target[currentTarget].transform.position - transform.position).normalized * 2);
        Debug.Log(agent.SetDestination(-(pathfindingScript.target[currentTarget].transform.position - transform.position).normalized * 2));
        /*
        rb.AddForce((retreatPosition.transform.position - transform.position) * 0.005f, ForceMode2D.Impulse);
        if (enemyAttackScript.distenceToTarget > whenToRetreat - 1 && enemyAttackScript.distenceToTarget  < whenToRetreat)
        {
            rb.velocity = Vector3.zero;
        }
        */
    }
}
