using Unity.VisualScripting;
using UnityEngine;

public class PlayerMeleeCombatPrototype : MonoBehaviour
{
    bool inputR1 = false;
    bool doesDamage = false;
    Animator animator;
    public int swordDamage;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            animator.SetTrigger("PressedR1");
            inputR1 = true;
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            inputR1 = false;
        }

        if (inputR1 == true)
        {
            doesDamage = true;
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                doesDamage = false;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(swordDamage);
                Debug.Log("Taken damage");
            }
            else
            {
                Debug.Log("Player did not take damage");
            }
        }
    }
}