using UnityEngine;
using UnityEngine.AI;

public class EnemyRetreat : MonoBehaviour
{

    Camera camera;

    Pathfinding pathfindingScript;
    EnemyAttacks enemyAttackScript;
    NavMeshAgent agent;

    int currentTarget;

    public float baseSpeed;
    public float retreatSpeed;
    public float whenToRetreat;

    bool canRetreat;

    Vector3 retreatDestination;
    [HideInInspector] public bool retreating;

    float cameraHorizontal;
    float cameraVertical;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfindingScript = GetComponent<Pathfinding>();
        enemyAttackScript = GetComponent<EnemyAttacks>();
        camera = Camera.main;

        Debug.Log(camera.orthographicSize*2);
        Debug.Log(camera.orthographicSize*2 * camera.aspect);
    }

    void Update()
    {
        cameraVertical = camera.orthographicSize * 2;
        cameraHorizontal = cameraVertical * camera.aspect;

        if (transform.position.x > cameraHorizontal/2 || transform.position.x < -cameraHorizontal/2)
        {
            canRetreat = false;
        }
        else if (transform.position.y > cameraVertical/2 || transform.position.y < -cameraVertical/2)
        {
            canRetreat = false;
        }
        else
        {
            canRetreat = true;
        }

        //retreat if enemy gets within distance
        currentTarget = pathfindingScript.FindClosestTarget(pathfindingScript.totalTargets);

        if (currentTarget < 0)
            return;

        retreatDestination = -(pathfindingScript.targetTransform - transform.position).normalized * whenToRetreat;
        if (enemyAttackScript.distenceToTarget < whenToRetreat && canRetreat == true)
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
