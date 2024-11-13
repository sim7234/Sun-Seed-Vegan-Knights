using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; 
    private int currentHealth;

    [SerializeField]
    private GameObject bloodOnHit;
    
    [SerializeField]
    private SpriteRenderer characterSprite;

    private Color baseColor;
    void Start()
    {
        currentHealth = maxHealth;
        baseColor = characterSprite.color;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(BlinkOnHit());
        GameObject newBlood = Instantiate(bloodOnHit, transform.position, Quaternion.identity);
        Destroy(newBlood, 0.8f);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
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