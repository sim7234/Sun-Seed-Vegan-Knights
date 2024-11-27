using System.Collections;
using UnityEngine;

public class enemyMeleeAttack : MonoBehaviour
{
    EnemyAttacks enemyAttacksScript;
    Pathfinding pathfindingScript;
    Damage attackPatternDamage;
    rotateTowardsTarget rotateScript;

    [SerializeField] GameObject attackPattern;
    SpriteRenderer damageSprite;

    [SerializeField] Color defaultColor;

    public float timeTillDamage;
    public float damageEnabledTime;

    bool isAttacking;

    float currentCooldown;
    float baseCooldown = 4f;

    // Start is called before the first frame update
    void Start()
    {
        enemyAttacksScript = GetComponent<EnemyAttacks>();
        pathfindingScript = GetComponent<Pathfinding>();
        attackPatternDamage = attackPattern.GetComponent<Damage>();
        damageSprite = attackPatternDamage.gameObject.GetComponent<SpriteRenderer>();
        rotateScript = attackPattern.GetComponentInParent<rotateTowardsTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (enemyAttacksScript.withinDistance == true && isAttacking == false && currentCooldown <= 0)
        {
            isAttacking = true;
            currentCooldown = baseCooldown;
            enemyAttacksScript.isAttacking = true;
            pathfindingScript.followTarget = false;

            attackPattern.SetActive(true);
            rotateScript.lockRotation = true;
            Invoke(nameof(StartMeleeAttack),1);
        }
    }

    void StartMeleeAttack()
    {
        attackPatternDamage.enabled = true;
        damageSprite.color = Color.black;
        Invoke(nameof(EndMeleeAttack), 1);
    }
    void EndMeleeAttack()
    {
        attackPatternDamage.enabled = false;
        rotateScript.lockRotation = false;
        damageSprite.color = defaultColor;
        attackPattern.SetActive(false);
        enemyAttacksScript.isAttacking = false;
        isAttacking = false;
    }
}
