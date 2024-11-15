using UnityEngine;

public class DashAttack : MonoBehaviour
{

    EnemyAttacks enemyAttacksScript;

    float directionToTarget;
    Pathfinding pathfindingScript;
    Vector3 targetPosition;

    bool dashing;

    // Start is called before the first frame update
    void Start()
    {
        pathfindingScript = GetComponent<Pathfinding>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAttacksScript.isAttacking == true)
        {
            if (!(pathfindingScript.target.Count <= 0))
            {
                targetPosition = pathfindingScript.target[pathfindingScript.finalTarget].transform.position;
            }
                

            directionToTarget = Vector3.Angle(targetPosition, transform.position);
            dashing = true;

            if (dashing == true)
            {



            }
            
        }
    }
}
