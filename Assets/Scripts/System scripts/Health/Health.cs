using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    public float maxHealth = 5;
    [SerializeField]
    public float currentHealth;
    [SerializeField]
    private GameObject bloodOnHit;
    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private SpriteRenderer characterSprite;

   [SerializeField]
    private List<AudioClip> hitSounds;
    private static int maxSimultaneousSounds = 5; 
    private static int currentPlayingSounds = 0;

    private AudioSource audioSource;

    private Color baseColor;

    private NavMeshAgent agent;

    [SerializeField]
    public bool EpelepticFilterOn;

    [SerializeField]
    public bool talkToMissionMaster = true;

    [HideInInspector]
    public Gamepad controllerPad;

    public bool endlessMode = false;
    private Coroutine stopRumbleAfterTimeCoroutine;

    [SerializeField]
    private GameObject onHitFrameChange;

<<<<<<< Updated upstream
    [SerializeField]
    private List<GameObject> bloodOnDeath = new List<GameObject>();

=======
    [SerializeField] GameObject waterDrop;

    public int WaterValue;
>>>>>>> Stashed changes
    void Start()
    {
        if (FindAnyObjectByType<MissionMaster>() == null)
        {
            talkToMissionMaster = false;
        }

        currentHealth = maxHealth;
        if (characterSprite != null)
        {
            if (onHitFrameChange != null)
            {
                baseColor = onHitFrameChange.GetComponent<SpriteRenderer>().color;
            }
            else
            {
                baseColor = characterSprite.color;
            }
        }
        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (GetComponent<NavMeshAgent>() != null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        EpelepticFilterOn = SaveData.Instance.epelepticFilterOn;
    }

    public void TakeDamage(float damageAmount)
    {
        StartCoroutine(BlinkOnHit());

        if (gameObject.CompareTag("Player"))
        {
            currentHealth -= 1;
            GetComponent<HeartHealthDisplay>()?.OnTakeDamage();
            GetComponent<Collider2D>().enabled = false;

            Invoke(nameof(TurnOnCollider), 1f);
        }
        else
        {
            currentHealth -= damageAmount;
        }

        PlayRandomHitSound(); 

        if (GetComponent<EnemyHealthDisplay>() != null)
        {
            GetComponent<EnemyHealthDisplay>().UpdateSprite();
            if (characterSprite != null)
            {
                baseColor = onHitFrameChange != null
                    ? onHitFrameChange.GetComponent<SpriteRenderer>().color
                    : characterSprite.color;
            }
        }

        if (bloodOnHit != null)
        {
            GameObject newBlood = Instantiate(bloodOnHit, transform.position, Quaternion.identity);
            Destroy(newBlood, 0.8f);
        }

        if (agent != null)
        {
            agent.velocity = Vector3.zero;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (gameObject.CompareTag("Player") && controllerPad != null)
        {
            RumblePulse(2.0f, 4.0f, 0.2f);
        }
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Heal(float amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            HeartHealthDisplay heartDisplay = GetComponent<HeartHealthDisplay>();
            if (heartDisplay != null)
            {
                heartDisplay.OnHeal();
            }
        }
    }

    private void PlayRandomHitSound()
    {
        if (hitSounds.Count > 0 && currentPlayingSounds < maxSimultaneousSounds)
        {
            AudioClip randomHitSound = hitSounds[Random.Range(0, hitSounds.Count)];
            if (audioSource != null)
            {
                currentPlayingSounds++; 

                audioSource.clip = randomHitSound;
                audioSource.pitch = Random.Range(0.8f, 1.2f); 
                audioSource.PlayOneShot(randomHitSound);

                StartCoroutine(ResetPlayingSound(randomHitSound.length)); 
        }
    }

private IEnumerator ResetPlayingSound(float clipLength)
{
    yield return new WaitForSeconds(clipLength);
    currentPlayingSounds = Mathf.Max(0, currentPlayingSounds - 1); 
}

    public void Die()
    {
        if (deathEffect != null)
        {
            GameObject newDeathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(newDeathEffect, 100f);
            if (bloodOnDeath.Count >= 1)
            {
                GameObject newGroundBlood = Instantiate(bloodOnDeath[Random.Range(0, bloodOnDeath.Count)], transform.position, Quaternion.identity);
            }
        }
        if (gameObject.CompareTag("Enemy") && talkToMissionMaster == true)
        {
            MissionMaster.Instance.EnemyKilled(this.gameObject);
        }

        Screenshake.Instance.Shake(2.0f, 0.2f, 1.0f);

        if (gameObject.CompareTag("Player"))
        {
            GetComponent<PlayerDeath>().onPlayerDeath();
        }
        else if (gameObject.CompareTag("Objective"))
        {
            GetComponent<Objective>().onObjectiveDeath();
        }
        else
        {
            if (endlessMode)
            {
                EndlessWaves.numberOfEnemies--;
                if (GetComponent<WorthScore>() != null)
                {
                    if (FindAnyObjectByType<Score>() != null)
                        FindAnyObjectByType<Score>().score += GetComponent<WorthScore>().howMuchScore;
                }
            }

            if (GetComponent<DropWaterOnDeath>() != null)
            {
                for (int i = 0; i < WaterValue; i++)
                {
                    Instantiate(waterDrop, transform.position, Quaternion.identity);
                }

            }

            Destroy(gameObject);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private IEnumerator BlinkOnHit()
    {
        if (onHitFrameChange != null)
        {
            onHitFrameChange.SetActive(true);
            characterSprite.enabled = false;

            if (!EpelepticFilterOn && characterSprite != null)
            {
                onHitFrameChange.GetComponent<SpriteRenderer>().color = Color.red;
                yield return new WaitForSeconds(0.2f);
                onHitFrameChange.GetComponent<SpriteRenderer>().color = baseColor;
                onHitFrameChange.SetActive(false);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                onHitFrameChange.SetActive(false);
            }
            characterSprite.enabled = true;
        }
        else
        {
            characterSprite.color = Color.red;
            yield return new WaitForSeconds(0.04f);
            characterSprite.color = baseColor;
        }
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        if (controllerPad != null)
        {
            controllerPad.SetMotorSpeeds(lowFrequency, highFrequency);
        }

        stopRumbleAfterTimeCoroutine = StartCoroutine(StopRumble(duration, controllerPad));
    }

    private IEnumerator StopRumble(float duration, Gamepad aPad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        aPad.SetMotorSpeeds(0, 0);
    }

    private void TurnOnCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}