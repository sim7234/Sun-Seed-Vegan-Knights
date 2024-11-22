using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SunFlowerTurret : MonoBehaviour
{

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private GameObject shootUpVisual;

    [SerializeField]
    private float shootInterval = 2.0f;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private float detectionRadius = 10.0f;

    private bool isActive;

    [SerializeField]
    private Animator sunHeadAnimator;

    [SerializeField]
    private GameObject sunGatheringEffect;

    [SerializeField]
    private AudioClip nastyGirl;

    private AudioSource nastyGirlSource;
    public void ActivateShooting()
    {
        isActive = true;
        StartCoroutine(Shoot());
    }

    private void Start()
    {
        ActivateShooting();
        nastyGirlSource = GetComponent<AudioSource>();
    }

    private IEnumerator Shoot()
    {
        while (isActive)
        {
            GameObject target = FindNearestEnemy();

            if (target != null)
            {
                sunGatheringEffect.SetActive(false);
                sunHeadAnimator.SetTrigger("Fire");
                Invoke(nameof(ShootUp), 1f);
                StartCoroutine(ShootAtTarget(target));
                yield return new WaitForSeconds(shootInterval);
            }

                yield return new WaitForSeconds(0.5f);
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = detectionRadius;

        foreach (GameObject Enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = Enemy;
                Debug.Log("Found Nearest Enemy");
            }
        }

        return nearestEnemy;
    }

    private void ShootUp()
    {
        GameObject projectile = Instantiate(shootUpVisual, transform.position, Quaternion.identity);
        Destroy(projectile, 3);
        nastyGirlSource.PlayOneShot(nastyGirl);
        
    }
    private IEnumerator ShootAtTarget(GameObject target)
    {
        yield return new WaitForSeconds(2);
        GameObject projectile;
        if (target != null)
        {
            projectile = Instantiate(projectilePrefab, target.transform.position, Quaternion.identity);
            Destroy(projectile, 10);
        }
        else
        {
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Destroy(projectile, 10);
        }
        Debug.Log("Shooting Enemy");

        yield return new WaitForSeconds(10);
        sunGatheringEffect.SetActive(true);
    }

    private void OnDestroy()
    {
        isActive = false;
    }
}
