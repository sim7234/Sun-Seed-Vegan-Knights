using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemyMeleeAttack : MonoBehaviour
{
    EnemyAttacks enemyAttacksScript;
    Pathfinding pathfindingScript;
    Collider2D attackVisualCollider;
    rotateTowardsTarget rotateScript;

    [SerializeField] GameObject attackPattern;
    SpriteRenderer damageSprite;

    [SerializeField] Color defaultColor;

    public float windUpTime;
    public float resetTime;

    bool cannotStartAttack;

    float currentCooldown;
    public float baseCooldown = 4f;

    bool canWalk = true;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        baseCooldown = baseCooldown + windUpTime + resetTime;

        enemyAttacksScript = GetComponent<EnemyAttacks>();
        pathfindingScript = GetComponent<Pathfinding>();
        attackVisualCollider = attackPattern.GetComponent<Collider2D>();
        damageSprite = attackVisualCollider.gameObject.GetComponent<SpriteRenderer>();
        rotateScript = attackPattern.GetComponentInParent<rotateTowardsTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canWalk == false)
        {
            agent.velocity = Vector3.zero;
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (enemyAttacksScript.withinDistance == true && cannotStartAttack == false && currentCooldown <= 0)
        {
            cannotStartAttack = true;
            currentCooldown = baseCooldown;

            enemyAttacksScript.isAttacking = true;
            pathfindingScript.followTarget = false;
            

            attackPattern.SetActive(true);
            rotateScript.lockRotation = true;
            canWalk = false;
            Invoke(nameof(StartMeleeAttack),windUpTime);
        }
    }

    void StartMeleeAttack()
    {
        
        attackVisualCollider.enabled = true;
        damageSprite.color = Color.black;
        Invoke(nameof(EndMeleeAttack), resetTime);
    }
    void EndMeleeAttack()
    {
        attackVisualCollider.enabled = false;
        rotateScript.lockRotation = false;

        damageSprite.color = defaultColor;
        attackPattern.SetActive(false);

        enemyAttacksScript.isAttacking = false;
        cannotStartAttack = false;
        canWalk = true;
    }
}
