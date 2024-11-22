using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    [SerializeField]
    [HideInInspector] public float currentHealth;

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

    [SerializeField]
    public bool EpelepticFilterOn;

    void Start()
    {
        currentHealth = maxHealth;
        baseColor = characterSprite.color;
        audioSource = GetComponent<AudioSource>();
        
        if (GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        EpelepticFilterOn = SaveData.Instance.epelepticFilterOn;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (GetComponent<EnemyHealthDisplay>() != null)
        {
            GetComponent<EnemyHealthDisplay>().UpdateSprite();
            baseColor = characterSprite.color;
        }

        if(gameObject.activeSelf)
        {
            StartCoroutine(BlinkOnHit());
        }
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
        }

        Screenshake.Instance.Shake(2.0f, 0.2f, 1.0f);
        
        if (gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            Invoke(nameof(Respawn), 5);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Respawn()
    {
        this.gameObject.SetActive(true);
        currentHealth = maxHealth;
       // this.gameObject.transform.position = Camera.main.transform.position;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private IEnumerator BlinkOnHit()
    {
        if(!EpelepticFilterOn)
        {
            characterSprite.color = Color.red;
            yield return new WaitForSeconds(0.01f);
            characterSprite.color = baseColor;
        }
    }
}