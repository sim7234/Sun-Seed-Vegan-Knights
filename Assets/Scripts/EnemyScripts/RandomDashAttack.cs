using UnityEngine;

public class RandomDashAttack : MonoBehaviour
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
    public int numberOfDashes;

    int currentDashAmount;

    float currentCooldown;

    bool beenInRange = false;

    Collider2D collider;

    [SerializeField] GameObject dashIndicator;

    Rigidbody2D rb;

    private Damage damageCompnent;
    Knockback knockbackscript;

    Camera camera;

    bool canDash;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        knockbackscript = GetComponent<Knockback>();
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        dashIndicator.SetActive(false);
        pathfindingScript = GetComponent<Pathfinding>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
        damageCompnent = GetComponent<Damage>();

        windUpTimer = 0f;
        knockbackscript.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (camera.WorldToViewportPoint(transform.position).x < 1 && camera.WorldToViewportPoint(transform.position).x > 0 &&
            camera.WorldToViewportPoint(transform.position).y < 1 && camera.WorldToViewportPoint(transform.position).y > 0)
        {
           canDash = true;
        }
        else
        {
            canDash = false;
            currentCooldown = dashCooldown;
        }



        if (currentCooldown >= 0 && canDash)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (enemyAttacksScript.withinDistance == true && currentCooldown <= 0)
        {
            beenInRange = true;
        }

        if (currentCooldown <= 0 && beenInRange == true)
        {
            CanDash();
        }
    }

    void CanDash()
    {
        if (pathfindingScript.target.Count <= 0)
            return;

        dashing = true;
        enemyAttacksScript.isAttacking = true;
        lockRotationLocation = transform.position;
        dashIndicator.SetActive(true);
        if (setup == true)
        {
            SetUpDash();
        }

        windUpTimer += Time.deltaTime;
        dashIndicator.transform.position = lockRotationLocation;

        if (windUpTimer >= dashWindupTime)
        {
            Dash();
        }

        if (windUpTimer >= dashWindupTime + dashTime)
        {
            currentDashAmount++;
            if (currentDashAmount >= numberOfDashes)
            {
                enemyAttacksScript.isAttacking = false;
                ResetDash();
            }
            else
            {
                dashAgain();
            }
        }

    }

    void Dash()
    {
        knockbackscript.enabled = true;
        damageCompnent.enabled = true;
        collider.isTrigger = true;
        dashIndicator.SetActive(false);
        rb.AddForce(dashIndicator.transform.up * dashPower, ForceMode2D.Impulse);
    }


    void ResetDash()
    {
        knockbackscript.enabled = false;
        collider.isTrigger = false;
        damageCompnent.enabled = false;
        rb.velocity = Vector2.zero;

        lockRotationLocation = transform.position;
        dashIndicator.transform.position = transform.position;

        windUpTimer = 0;
        setup = true;
        dashing = false;

        currentCooldown = dashCooldown;

        beenInRange = false;
    }

    void SetUpDash()
    {
        if (pathfindingScript.target[pathfindingScript.randomTarget] != null)
            targetPosition = pathfindingScript.target[pathfindingScript.FindRandomTarget(pathfindingScript.totalTargets)].transform.position;

        targetPosition.x = targetPosition.x - transform.position.x;
        targetPosition.y = targetPosition.y - transform.position.y;

        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        dashIndicator.transform.rotation = Quaternion.Euler(0, 0, angle + -90);

        lockRotationLocation = transform.position;
        setup = false;
    }


    void dashAgain()
    {
        setup = true;

        rb.velocity = Vector2.zero;
        lockRotationLocation = transform.position;
        dashIndicator.transform.position = transform.position;
    }
}
