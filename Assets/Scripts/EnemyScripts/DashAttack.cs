using UnityEngine;

public class DashAttack : MonoBehaviour
{

    EnemyAttacks enemyAttacksScript;

    float directionToTarget;
    Pathfinding pathfindingScript;
    Vector3 targetPosition;
    Vector2 lockRotationLocation;

    bool dashing;
    bool setup = true;

    float windUpTimer = 0;
    //Hide to lower compile time.
     public float dashWindupTime = 0.45f;
     public float dashTime = 0.4f;
     public float dashPower = 2;
     public float dashCooldown = 2;

    float currentCooldown;

    bool beenInRange = false;

    Collider2D collider;

    [SerializeField] GameObject dashRotationPoint;

    Rigidbody2D rb;

    private Damage damageCompnent;
    Knockback knockbackscript;

    [SerializeField]
    private AudioClip dashSound;

    private AudioSource audioSource;
    private bool audioIsPlayed;

    // Start is called before the first frame update
    void Start()
    {
        knockbackscript = GetComponent<Knockback>();
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        dashRotationPoint.SetActive(false);
        pathfindingScript = GetComponent<Pathfinding>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
        damageCompnent = GetComponent<Damage>();

        windUpTimer = 0f;
        knockbackscript.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentCooldown <= 0 && beenInRange == true)
        {
            CanDash();
        }

        if (enemyAttacksScript.withinDistance == true && currentCooldown <= 0)
        {
            beenInRange = true;
        }


    }

    void CanDash()
    {
        if (enemyAttacksScript.withinDistance == true)
        {
            if (pathfindingScript.target.Count <= 0)
                return;
            
            dashing = true;
            enemyAttacksScript.isAttacking = true;
            if(pathfindingScript.target[pathfindingScript.finalTarget] != null)
            targetPosition = pathfindingScript.target[pathfindingScript.finalTarget].transform.position;
        } 

        if (dashing == true)
        {
            lockRotationLocation = transform.position;
            dashRotationPoint.SetActive(true);

            if (setup == true)
            {
                SetUpDash();
            }

            windUpTimer += Time.deltaTime;
            dashRotationPoint.transform.position = lockRotationLocation;

            if (windUpTimer >= dashWindupTime )
            {
                if(!audioIsPlayed)
                {
                    audioSource.PlayOneShot(dashSound);
                    audioIsPlayed = true;
                }
                Dash();
            }

            if (windUpTimer >= dashWindupTime + dashTime)
            {
                enemyAttacksScript.isAttacking = false;
                ResetDash();
            }
        }
    }

    void Dash()
    {
        knockbackscript.enabled = true;
        damageCompnent.enabled = true;
        collider.isTrigger = true;
        dashRotationPoint.SetActive(false);
        rb.AddForce(dashRotationPoint.transform.up * dashPower, ForceMode2D.Impulse);
    }


    void ResetDash()
    {
        audioIsPlayed = false;
        knockbackscript.enabled = false;
        //collider.isTrigger = false;
        damageCompnent.enabled = false;
        rb.velocity = Vector2.zero;

        lockRotationLocation = transform.position;
        dashRotationPoint.transform.position = transform.position;

        windUpTimer = 0;
        setup = true;
        dashing = false;

        currentCooldown = dashCooldown;

        beenInRange = false;
    }

    void SetUpDash()
    {
        targetPosition.x = targetPosition.x - transform.position.x;
        targetPosition.y = targetPosition.y - transform.position.y;

        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        dashRotationPoint.transform.rotation = Quaternion.Euler(0, 0, angle + -90);

        lockRotationLocation = transform.position;
        setup = false;
    }


}
