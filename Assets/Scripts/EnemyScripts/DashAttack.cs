using UnityEngine;

public class DashAttack : MonoBehaviour
{

    EnemyAttacks enemyAttacksScript;

    float directionToTarget;
    Pathfinding pathfindingScript;
    Vector3 targetPosition;
    Vector2 lockRotationLocation;

    float rotationSpeed;

    bool dashing;
    bool setup = true;

    float windUpTimer = 0;
    public float dashWindupTime;
    public float dashTime = 0.5f;
    public float dashPower;

    public float dashCooldown;
    float currentCooldown;

    Collider2D collider;

    [SerializeField] GameObject dashIndicator;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        windUpTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        rotationSpeed = 0;
        dashIndicator.SetActive(false);
        pathfindingScript = GetComponent<Pathfinding>();
        enemyAttacksScript = GetComponent<EnemyAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown >= 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (currentCooldown <= 0)
        {
            CanDash();
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
            targetPosition = pathfindingScript.target[pathfindingScript.finalTarget].transform.position;
        }
        else
        {
            enemyAttacksScript.isAttacking = false;
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

            if (windUpTimer >= dashWindupTime)
            {
                Dash();
            }

            if (windUpTimer >= dashWindupTime + dashTime)
            {
                ResetDash();
            }
        }
    }

    void Dash()
    {
        collider.isTrigger = true;
        rb.AddForce(dashIndicator.transform.up * dashPower);
        dashIndicator.SetActive(false);
    }


    void ResetDash()
    {
        collider.isTrigger = false;
        rb.velocity = Vector2.zero;

        lockRotationLocation = transform.position;
        dashIndicator.transform.position = transform.position;

        windUpTimer = 0;
        setup = true;
        dashing = false;

        currentCooldown = dashCooldown;
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
