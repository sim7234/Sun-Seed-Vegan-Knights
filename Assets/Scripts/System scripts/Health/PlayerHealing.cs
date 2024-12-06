using UnityEngine;

public class PlayerHealing : MonoBehaviour
{
    private Health health;
    [SerializeField]
    private float healingAmount;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if (GetComponent<EnteredSun>().inSun)
        {
            health.Heal(healingAmount * Time.deltaTime);
        }
    }

    public void HealMax()
    {

        health.currentHealth = health.maxHealth;

    }
}
