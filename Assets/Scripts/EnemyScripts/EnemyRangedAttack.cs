using System.Collections;
using Unity.VisualScripting;
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

    [SerializeField] SpriteRenderer eye;

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
    }

    // Update is called once per frame
    void Update()
    {
        camera = Camera.main;
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

        Vector2 distenceToCamera = camera.WorldToViewportPoint(gameObject.transform.position);

        if(distenceToCamera.x < 0 || distenceToCamera.y < 0)
        {
            enemyAttacksScript.distanceToAttack = 0;
            canShoot = false;
        }
        else
        {
            enemyAttacksScript.distanceToAttack = distenceToAttack;
            canShoot = true;
        }
    }

   

    void rangedAttack()
    {

        StartCoroutine(nameof(ProjectileWindpTime));
        canShoot = false;

        spawnPointToVector = projectileSpawnPoint.transform.position;

         
    }


    private IEnumerator ProjectileWindpTime()
    {
        

        eye.color = Color.magenta;
        yield return new WaitForSeconds(1);
        GameObject projectile = Instantiate(projectilePrefab, spawnPointToVector, Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().AddForce((targetPos + new Vector3(-1,0)) * projectileSpeed, ForceMode2D.Impulse);
        attackCooldown = attackSpeed;
        eye.color = Color.white;

    }
    

    
}
