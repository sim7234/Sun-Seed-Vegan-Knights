using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    Health healthScript;
    GameObject Sword;
    PlayerMeleeCombatPrototype meleeScript;

    private void Start()
    {
        Sword = GameObject.FindWithTag("Sword");
        meleeScript = Sword.GetComponent<PlayerMeleeCombatPrototype>();
        healthScript = GetComponent<Health>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Test");
        if (other.gameObject.CompareTag("Sword"))
        {
         //   healthScript.TakeDamage(meleeScript.swordDamage);
            Debug.Log("Hit!");
        }
    }
}
