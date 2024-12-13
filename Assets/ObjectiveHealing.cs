using UnityEngine;
public class ObjectiveHealing : MonoBehaviour
{

    Health healthScript;

    public float healAmount;
    public float healCooldown;

    float timer;
    void Start()
    {
        if (GetComponent<Health>() != null)
        healthScript = GetComponent<Health>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }    

        if (healthScript != null)
        {
            if (healthScript.currentHealth < healthScript.maxHealth && timer <= 0)
            {
                healthScript.currentHealth += healAmount;
                timer = healCooldown;
            }
        }
    }
}
