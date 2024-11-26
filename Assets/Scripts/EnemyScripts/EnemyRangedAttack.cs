using UnityEngine;

public class EnemyRangedAttack : Pathfinding
{
    EnemyAttacks enemyAttacksScript;
    EnemyRetreat enemyRetreatScript;

    [HideInInspector] public bool rangedAttackActive;

    [SerializeField] GameObject projectilePrefab;

    public float attackSpeed;
    public float damage;
    public float projectileSpeed;

    float attackCooldown;
    Vector3 rangedTarget;
    void Start()
    {
        enemyRetreatScript = GetComponent<EnemyRetreat>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAttacksScript == null) return;
        if (enemyRetreatScript == null) return;

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
        if (enemyAttacksScript.withinDistance != true)
        {
           rangedTarget = target[finalTarget].transform.position;
        }
        GameObject projectile = Instantiate(projectilePrefab,(rangedTarget - transform.position).normalized * 0.1f, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddForce((rangedTarget - transform.position) * projectileSpeed, ForceMode2D.Impulse);
        attackCooldown = attackSpeed;
    }

}
