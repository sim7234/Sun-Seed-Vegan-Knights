using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaterObjective : MonoBehaviour
{
    [SerializeField] private float maxWater = 1500f;
    private float currentWater = 0f;

    [SerializeField] private float waterDepletionRate = 5f;
    private bool isComplete = false;

    [SerializeField] private TMP_Text waterProgressText;

    [SerializeField] private LineRenderer tongueLine;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootInterval = 0.5f;
    [SerializeField] private float detectionRadius = 10.0f;

    [SerializeField] private Animator frogAnimator;
    [SerializeField] private SpriteRenderer frogSpriteRenderer;
    [SerializeField] private Sprite frogOpenMouthSprite;

    private bool isTurretActive = false;

    private void Start()
    {
        currentWater = 1; // starting water
        UpdateProgressUI();
        StartCoroutine(DepleteWater());
    }

    private void UpdateProgressUI()
    {
        if (waterProgressText != null)
        {
            waterProgressText.text = $"{Mathf.FloorToInt(currentWater)} / {Mathf.FloorToInt(maxWater)}";
        }
    }

    public void AddWater(float amount)
    {
        if (!isComplete)
        {
            currentWater += amount;
            currentWater = Mathf.Clamp(currentWater, 0, maxWater);
            UpdateProgressUI();

            if (currentWater >= maxWater)
            {
                CompleteObjective();
            }
        }
    }

    private void CompleteObjective()
    {
        isComplete = true;
        StopCoroutine(DepleteWater());
        ActivateTurret();
    }

    private IEnumerator DepleteWater()
    {
        while (!isComplete)
        {
            yield return new WaitForSeconds(1f);

            if (currentWater >= maxWater || currentWater <= 0)
            {
                continue;
            }
            currentWater -= waterDepletionRate;
            currentWater = Mathf.Max(currentWater, 0);
            UpdateProgressUI();

            if (currentWater <= 0)
            {
                break;
            }
        }
    }

    private void ActivateTurret()
    {
        isTurretActive = true;
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (isTurretActive)
        {
            GameObject target = FindNearestEnemy();

            if (target != null)
            {
                yield return StartCoroutine(ShootAtTarget(target));
            }

            yield return new WaitForSeconds(shootInterval);
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = detectionRadius;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private IEnumerator ShootAtTarget(GameObject target)
    {
        if (target == null || !target.activeInHierarchy)
        {
            yield break; 
        }
        if (tongueLine != null && shootPoint != null)
        {
            if (frogAnimator != null)
            {
                frogAnimator.enabled = false;
            }
            if (frogSpriteRenderer != null && frogOpenMouthSprite != null)
            {
                frogSpriteRenderer.sprite = frogOpenMouthSprite;

                float shootSpeed = 120f;
                Vector2 startPosition = shootPoint.position;
                Vector2 endPosition = target != null ? target.transform.position : startPosition;
                float distance = Vector2.Distance(startPosition, endPosition);
                float time = 0;

                tongueLine.SetPosition(0, startPosition);

                float maxTongueDuration = 5f; 
                float elapsedTongueTime = 0f;

                while (time < distance / shootSpeed && elapsedTongueTime < maxTongueDuration)
                {
                    if (target == null || !target.activeInHierarchy)
                    {
                        break;
                    }

                    time += Time.deltaTime;
                    elapsedTongueTime += Time.deltaTime;
                    Vector2 currentPoint = Vector2.Lerp(startPosition, endPosition, time / (distance / shootSpeed));
                    tongueLine.SetPosition(1, new Vector3(currentPoint.x, currentPoint.y, 0));
                    yield return null;
                }

                tongueLine.SetPosition(1, endPosition);

                if (target != null)
                {
                    Health targetHealth = target.GetComponent<Health>();
                    if (targetHealth != null)
                    {
                        Debug.Log($"Target Health Detected: {targetHealth.GetCurrentHealth()}");
                        if (targetHealth.GetCurrentHealth() <= 5)
                        {
                            targetHealth.TakeDamage(150);
                        }
                        else if (targetHealth.GetCurrentHealth() <= 150)
                        {
                            yield return StartCoroutine(DragTargetToFrog(target));
                            targetHealth.TakeDamage(150);
                        }
                        else
                        {
                            targetHealth.TakeDamage(150);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No health component found on target.");
                    }
                }

                yield return new WaitForSeconds(0.1f);
                tongueLine.SetPosition(0, Vector3.zero);
                tongueLine.SetPosition(1, Vector3.zero);

                if (frogAnimator != null)
                {
                    frogAnimator.enabled = true;
                }
            }
        }
    }
    private IEnumerator DragTargetToFrog(GameObject target)
    {
        if (target == null || !target.activeInHierarchy)
        {
            Debug.LogWarning("Target is null or inactive. Exiting drag logic.");
            yield break; 
        }

        BloomRecipient bloomRecipient = target.GetComponent<BloomRecipient>();
        if (bloomRecipient != null)
        {
            bloomRecipient.ResetForDrag();
        }
        Debug.Log("Starting drag logic for target: " + target.name);
        
        if (frogAnimator != null)
        {
            frogAnimator.enabled = false;
        }
        if (frogSpriteRenderer != null && frogOpenMouthSprite != null)
        {
            frogSpriteRenderer.sprite = frogOpenMouthSprite;
        }

        Vector3 startPosition = target.transform.position;
        Vector3 endPosition = shootPoint.position;
        float dragSpeed = 0.6f;
        float time = 0;

        if (target.TryGetComponent(out Rigidbody2D rb))
        {
            Debug.Log("Found Rigidbody2D on target.");
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        Collider2D frogCollider = GetComponent<Collider2D>();
        Collider2D targetCollider = target.GetComponent<Collider2D>();
        if (frogCollider != null && targetCollider != null)
        {
            Debug.Log("Ignoring collisions between frog and target.");
            Physics2D.IgnoreCollision(frogCollider, targetCollider, true);
        }

        while (time < 1f)
        {
            if (target == null || !target.activeInHierarchy)
            {
                Debug.LogWarning("Target became null or inactive during drag.");
                yield break; 
            }

            time += Time.deltaTime * dragSpeed;
            Vector3 currentTargetPosition = Vector3.Lerp(startPosition, endPosition, time);
            target.transform.position = currentTargetPosition;

            tongueLine.SetPosition(0, shootPoint.position);
            tongueLine.SetPosition(1, currentTargetPosition);

            yield return null;
        }

        if (frogCollider != null && targetCollider != null)
        {
            Debug.Log("Re-enabling collisions between frog and target.");
            Physics2D.IgnoreCollision(frogCollider, targetCollider, false);
        }

        tongueLine.SetPosition(0, Vector3.zero);
        tongueLine.SetPosition(1, Vector3.zero);

        if (frogAnimator != null)
        {
            frogAnimator.enabled = true;
        }

        Debug.Log("Drag logic complete for target: " + target.name);
    }
}

