using UnityEngine;

public class explodeOnContact : MonoBehaviour
{
    bool startExplosion;
    float explosionTimer = 0.2f;
    float timeTillExplosion;

    SpriteRenderer kottBulleSprite;

    [SerializeField] Collider2D explosionCollider;
    EnemyAttacks enemyAttacksScript;

    [SerializeField]
    private GameObject normalSprite;
    [SerializeField]
    private GameObject explosionAnimeSprite;
    [SerializeField]
    private GameObject explosionVFX;
    float growthRate = 0.1f;

    private void Start()
    {
        enemyAttacksScript = GetComponent<EnemyAttacks>();
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
            normalSprite.SetActive(false);
            explosionAnimeSprite.SetActive(true);
            if (timeTillExplosion <= 0.1)
            {
                explosionVFX.SetActive(true);
                explosionCollider.gameObject.SetActive(true);
            }
            //kottBulleSprite.color = Color.red;
            
            if (timeTillExplosion > 0)
            {
                timeTillExplosion -= Time.deltaTime;
            }

            explosionAnimeSprite.transform.localScale = new Vector3(
                explosionAnimeSprite.transform.localScale.x + Time.deltaTime * growthRate,
                explosionAnimeSprite.transform.localScale.y + Time.deltaTime * growthRate
            );

            if (timeTillExplosion <= 0)
            {
                GetComponent<Health>().currentHealth = 0;
                GetComponent<Health>().Die();
            }
        }
    }
}