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

    [SerializeField] GameObject dashIndicator;

    Rigidbody2D rb;

    private Damage damageCompnent;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        windUpTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        dashIndicator.SetActive(false);
        pathfindingScript = GetComponent<Pathfinding>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
        damageCompnent = GetComponent<Damage>();
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
            dashIndicator.SetActive(true);

            if (setup == true)
            {
                SetUpDash();
            }

            windUpTimer += Time.deltaTime;
            dashIndicator.transform.position = lockRotationLocation;

            if (windUpTimer >= dashWindupTime )
            {
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
        //damageCompnent.enabled = true;
        collider.isTrigger = true;
        dashIndicator.SetActive(false);
        rb.AddForce(dashIndicator.transform.up * dashPower, ForceMode2D.Impulse);
    }


    void ResetDash()
    {
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
        targetPosition.x = targetPosition.x - transform.position.x;
        targetPosition.y = targetPosition.y - transform.position.y;

        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        dashIndicator.transform.rotation = Quaternion.Euler(0, 0, angle + -90);

        lockRotationLocation = transform.position;
        setup = false;
    }


}
