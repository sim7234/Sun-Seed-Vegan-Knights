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

    [SerializeField] GameObject dashIndicator;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
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
            dashIndicator.SetActive(true);
            if (setup == true)
            {
                setUpDash();
            }
            windUpTimer += Time.deltaTime;
            dashIndicator.transform.position = lockRotationLocation;

            if (windUpTimer >= dashWindupTime)
            {
                rb.AddForce(dashIndicator.transform.up * 100);
            }
        }



    }

    void setUpDash()
    {
        targetPosition.x = targetPosition.x - transform.position.x;
        targetPosition.y = targetPosition.y - transform.position.y;

        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        dashIndicator.transform.rotation = Quaternion.Euler(0, 0, angle + -90);

        lockRotationLocation = transform.position;
        setup = false;
    }


}
