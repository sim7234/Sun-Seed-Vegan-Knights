using UnityEngine;

public class EndlessScaleWithWaves : MonoBehaviour
{

    EndlessWaves wavesScript;
    Health healthScript;
    DashAttack attackScript;
    WorthScore worthScoreScript;

    float baseHealth;
    float currentHealth;

    float baseAttackCooldown;
    float currentAttackCooldown;

    int worthScore;

    float dashWindupTimeBase;
    float dashWindupTimeCurrent;

    [SerializeField] int increaseScoreBy;
    [SerializeField] float minimumCooldown;

    // Start is called before the first frame update
    void Start()
    {
        wavesScript = FindAnyObjectByType<EndlessWaves>();
        healthScript = gameObject.GetComponent<Health>();
        attackScript = gameObject.GetComponent<DashAttack>();

        baseHealth = healthScript.maxHealth;
        currentHealth = baseHealth;

        baseAttackCooldown = attackScript.dashCooldown;
        currentAttackCooldown = baseAttackCooldown;

        dashWindupTimeBase = attackScript.dashWindupTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (wavesScript.buffDashEnemy == true)
        {
            wavesScript.buffDashEnemy = false;
            currentHealth += baseHealth + wavesScript.difficultyMultiplier * 50;
            currentAttackCooldown = baseAttackCooldown - (wavesScript.difficultyMultiplier * 0.5f);
            currentAttackCooldown = Mathf.Clamp(currentAttackCooldown, minimumCooldown, Mathf.Infinity);

            dashWindupTimeCurrent = dashWindupTimeBase - wavesScript.difficultyMultiplier * 0.3f;
            dashWindupTimeCurrent = Mathf.Clamp(dashWindupTimeCurrent,0.1f, Mathf.Infinity);


           // healthScript.maxHealth = currentHealth;
            //attackScript.dashCooldown = currentAttackCooldown;
            attackScript.dashWindupTime = dashWindupTimeCurrent;

        }
    }
}
