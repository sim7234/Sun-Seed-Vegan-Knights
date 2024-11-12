using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    Health healthScript;

    private void Start()
    {
        healthScript = GetComponent<Health>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Damage!!!");
        if (other.gameObject.CompareTag("Sword"))
        {
            healthScript.TakeDamage(other.gameObject.GetComponent<PlayerMeleeCombatPrototype>().swordDamage);
            Debug.Log("Hit!");
        }
    }

}
