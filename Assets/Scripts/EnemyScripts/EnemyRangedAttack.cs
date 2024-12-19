using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyRangedAttack : MonoBehaviour
{
    EnemyAttacks enemyAttacksScript;
    EnemyRetreat enemyRetreatScript;
    Pathfinding pathfindingScript;
    rotateTowardsTarget rotateScript;

    [HideInInspector] public bool rangedAttackActive;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] Transform rotationPoint;

    [SerializeField] float distenceToAttack;

    [SerializeField] SpriteRenderer eye;
    [SerializeField] Transform pupil;

    LineRenderer laserLine;

    public float attackSpeed;
    public float projectileSpeed;

    Vector3 spawnPointToVector;

    float attackCooldown;
    Vector3 rangedTarget;
    Vector3 targetPos;
    Vector3 laserTargetPos;

    int targetArrayPos;

    Camera camera;
    float cameraVertical;
    float cameraHorizontal;

    bool withinCameraCanShoot;
    float totalWindUpTime;
    float windUpTimer;

    bool canShoot = true;
    bool changeColorForEye;
    float colorValueOne;

    float angle;
    void Start()
    {
        rotateScript = GetComponentInChildren<rotateTowardsTarget>();
        laserLine = GetComponent<LineRenderer>();
        pathfindingScript = GetComponent<Pathfinding>();
        enemyRetreatScript = GetComponent<EnemyRetreat>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeColorForEye == true)
        {
            rotateScript.lockRotation = true;
            colorValueOne -= Time.deltaTime * 3;

            float rnd = Random.Range(-0.01f, 0.02f);
        }
        else
        {
            rotateScript.lockRotation = false;
            colorValueOne += Time.deltaTime * 5;
        }
        colorValueOne = Mathf.Clamp01(colorValueOne);

        eye.color = new Color(1, colorValueOne, colorValueOne);

        camera = Camera.main;
        if (enemyAttacksScript == null) return;
        if (enemyRetreatScript == null) return;
        if (pathfindingScript == null) return;

        targetPos = pathfindingScript.targetTransform;
        laserTargetPos = targetPos;


        targetPos.x = targetPos.x - transform.position.x;
        targetPos.y = targetPos.y - transform.position.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
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

            if (attackCooldown <= 0 && withinCameraCanShoot == true && canShoot == true)
            {
                canShoot = false;
                changeColorForEye = true;
                withinCameraCanShoot = false;
                windUpTimer = totalWindUpTime;
                Invoke(nameof(rangedAttack),1);
            }
        }
        else
        {
            rangedAttackActive = false;
        }

        Vector2 distenceToCamera = camera.WorldToViewportPoint(gameObject.transform.position);

        if (distenceToCamera.x < 0 || distenceToCamera.y < 0)
        {
            enemyAttacksScript.distanceToAttack = 0;
            withinCameraCanShoot = false;
        }
        else
        {
            enemyAttacksScript.distanceToAttack = distenceToAttack;
            withinCameraCanShoot = true;
        }
    }



    void rangedAttack()
    {
        spawnPointToVector = projectileSpawnPoint.transform.position;

        GameObject projectile = Instantiate(projectilePrefab, spawnPointToVector, Quaternion.identity);

        projectile.transform.rotation = Quaternion.Euler(0, 0, (angle - 90) +180);

        projectile.GetComponent<Rigidbody2D>().AddForce(targetPos * projectileSpeed, ForceMode2D.Impulse);
        attackCooldown = attackSpeed;
        changeColorForEye = false;
        canShoot = true;
    }
}
