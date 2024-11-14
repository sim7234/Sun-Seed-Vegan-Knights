using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth;

    [SerializeField]
    private GameObject bloodOnHit;
    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private SpriteRenderer characterSprite;

    [SerializeField]
    private AudioClip hitSound;

    private AudioSource audioSource;

    private Color baseColor;

    private NavMeshAgent agent;
    void Start()
    {
        currentHealth = maxHealth;
        baseColor = characterSprite.color;
        audioSource = GetComponent<AudioSource>();
        
        if (GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(BlinkOnHit());
        GameObject newBlood = Instantiate(bloodOnHit, transform.position, Quaternion.identity);
        Destroy(newBlood, 0.8f);
        audioSource.pitch = Random.Range(0.90f, 1.1f);
        audioSource.Play();
        
        if(agent != null)
        {
            agent.velocity = Vector3.zero;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject newDeathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(newDeathEffect, 0.8f);
        if(gameObject.CompareTag("Enemy"))
        {
            MissionMaster.Instance.EnemyKilled();
            Debug.Log("enemy killed:  " + gameObject.name);
        }
        Destroy(gameObject);
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private IEnumerator BlinkOnHit()
    {
        characterSprite.color = Color.red;
        yield return new WaitForSeconds(0.01f);
        characterSprite.color = baseColor;
    }
}