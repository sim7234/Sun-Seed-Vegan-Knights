using UnityEngine;

public class explodeOnContact : MonoBehaviour
{
    bool startExplosion;
    float explosionTimer = 0.5f;
    float timeTillExplosion;

    SpriteRenderer kottBulleSprite;

    [SerializeField] Collider2D explosionCollider;
    SpriteRenderer explosionSprite;
    EnemyAttacks enemyAttacksScript;

    float growthRate = 0.1f;

    private void Start()
    {
        enemyAttacksScript = GetComponent<EnemyAttacks>();
        explosionSprite = explosionCollider.gameObject.GetComponent<SpriteRenderer>();
        kottBulleSprite = GetComponent<SpriteRenderer>();

        timeTillExplosion = explosionTimer;
    }

    private void Update()
    {
        if (enemyAttacksScript.withinDistance == true)
        {
            startExplosion = true;
        }

        if (startExplosion == true)
        {
            if (timeTillExplosion <= 0.1)
            {
                explosionCollider.gameObject.SetActive(true);
                explosionSprite.enabled = true;
            }

            kottBulleSprite.color = Color.red;
            if (timeTillExplosion > 0)
            {
                timeTillExplosion -= Time.deltaTime;
            }

            transform.localScale = new Vector3(
                transform.localScale.x + Time.deltaTime * growthRate,
                transform.localScale.y + Time.deltaTime * growthRate
            );

            if (timeTillExplosion <= 0)
            {
                NotifyMissionMaster();
                Destroy(gameObject);
            }
        }
    }

    private void NotifyMissionMaster()
    {
        if (MissionMaster.Instance != null)
        {
            MissionMaster.Instance.EnemyKilled(gameObject);
        }
    }
}