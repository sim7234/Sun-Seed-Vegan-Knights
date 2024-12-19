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

    [SerializeField]
    private GameObject walkingSprite;
    [SerializeField]
    private GameObject attackingSprite;

    [SerializeField]
    private GameObject attackEffectPos;
    [SerializeField]
    private GameObject attackPS;
    [SerializeField]
    private GameObject rotationReference;

    [SerializeField] AudioClip attackSound;

    AudioSource audioSourceSaveData;

    EnemyTurnTowardsTarget[] flipSpriteScript = new EnemyTurnTowardsTarget[2];



    // Start is called before the first frame update
    void Start()
    {
        flipSpriteScript = GetComponents<EnemyTurnTowardsTarget>();

        audioSourceSaveData = FindAnyObjectByType<SaveData>().GetComponentInChildren<AudioSource>();
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

            foreach (var flipScript in flipSpriteScript)
            {
                flipScript.canFlip = false;
            }
            cannotStartAttack = true;
            currentCooldown = baseCooldown;

            enemyAttacksScript.isAttacking = true;
            pathfindingScript.followTarget = false;


            attackPattern.SetActive(true);
            rotateScript.lockRotation = true;
            canWalk = false;
            changeSprite();
            Invoke(nameof(StartMeleeAttack), windUpTime);
        }
        attackEffectPos.transform.localEulerAngles = new Vector3(0, 0, rotationReference.transform.localEulerAngles.z);
    }

    void StartMeleeAttack()
    {
        
        audioSourceSaveData.PlayOneShot(attackSound);
        attackVisualCollider.enabled = true;
        damageSprite.color = Color.black;
        Invoke(nameof(EndMeleeAttack), resetTime);
    }
    void EndMeleeAttack()
    {
        foreach (var flipScript in flipSpriteScript)
        {
            flipScript.canFlip = true;
        }
        attackVisualCollider.enabled = false;
        rotateScript.lockRotation = false;

        damageSprite.color = defaultColor;
        attackPattern.SetActive(false);

        enemyAttacksScript.isAttacking = false;
        cannotStartAttack = false;
        canWalk = true;
        changeSprite();
    }

    private void changeSprite()
    {
        if (walkingSprite.activeSelf)
        {
            walkingSprite.SetActive(false);
            attackingSprite.SetActive(true);
        }
        else
        {
            walkingSprite.SetActive(true);
            attackingSprite.SetActive(false);
            attackPS.SetActive(false);
        }
        
    }

}
