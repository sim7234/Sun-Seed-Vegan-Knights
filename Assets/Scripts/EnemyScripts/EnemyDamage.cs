using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10; 


    //Teodor, Explain why you change to Trigger Enter instead of collision Enter
     
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Health playerHealth = collision.gameObject.GetComponent<Health>();
            
    //        if (playerHealth != null)
    //        {
    //            playerHealth.TakeDamage(damageAmount);
    //            Debug.Log("Taken damage");
    //        }
    //        else
    //        {
    //            Debug.Log("Player did not take damage");
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Taken damage");
            }
            else
            {
                Debug.Log("Player did not take damage");
            }
        }
    }
}