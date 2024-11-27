using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    EnemyAttacks enemyAttacksScript;
    EnemyRetreat enemyRetreatScript;
    Pathfinding pathfindingScript;

    [HideInInspector] public bool rangedAttackActive;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] Transform rotationPoint;

    public float attackSpeed;
    public float projectileSpeed;

    Vector3 spawnPointToVector;

    float attackCooldown;
    Vector3 rangedTarget;
    Vector3 targetPos;

    int targetArrayPos;
    void Start()
    {
        pathfindingScript = GetComponent<Pathfinding>();
        enemyRetreatScript = GetComponent<EnemyRetreat>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAttacksScript == null) return;
        if (enemyRetreatScript == null) return;
        if (pathfindingScript == null) return;

        targetPos = pathfindingScript.targetTransform;


        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        rotationPoint.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        
        if (enemyAttacksScript.withinDistance == true && enemyRetreatScript.retreating == false)
        {
            rangedAttackActive = true;
            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }

            if (attackCooldown <= 0)
            {
                rangedAttack();
            }
        }
        else
        { 
            rangedAttackActive = false;
        }
    }
    void rangedAttack()
    {
        spawnPointToVector = projectileSpawnPoint.transform.position;

         GameObject projectile = Instantiate(projectilePrefab, spawnPointToVector, Quaternion.identity);

         projectile.GetComponent<Rigidbody2D>().AddForce((targetPos) * projectileSpeed, ForceMode2D.Impulse);
         attackCooldown = attackSpeed; 
    }
}
