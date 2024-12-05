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

    [SerializeField] float distenceToAttack;

    public float attackSpeed;
    public float projectileSpeed;

    Vector3 spawnPointToVector;

    float attackCooldown;
    Vector3 rangedTarget;
    Vector3 targetPos;

    int targetArrayPos;

    Camera camera;
    float cameraVertical;
    float cameraHorizontal;

    bool canShoot;
    void Start()
    {
        pathfindingScript = GetComponent<Pathfinding>();
        enemyRetreatScript = GetComponent<EnemyRetreat>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
        camera = Camera.main;
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


        cameraVertical = camera.orthographicSize * 2;
        cameraHorizontal = cameraVertical * camera.aspect;

        if (transform.position.x > (cameraHorizontal / 2 - 0.7) || transform.position.x < -(cameraHorizontal / 2 - 0.7))
        {
            enemyAttacksScript.distanceToAttack = 0;
            canShoot = false;
        }
        else if (transform.position.y > ((cameraVertical / 2) - 0.7) || transform.position.y < -((cameraVertical / 2 - 0.7)))
        {
            enemyAttacksScript.distanceToAttack = 0;
            canShoot = false;
        }
        else
        {
            enemyAttacksScript.distanceToAttack = distenceToAttack;
            canShoot = true;
        }

        if (enemyAttacksScript.withinDistance == true && enemyRetreatScript.retreating == false)
        {
            rangedAttackActive = true;
            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }

            if (attackCooldown <= 0 && canShoot == true)
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
