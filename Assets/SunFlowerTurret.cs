using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerTurret : MonoBehaviour
{

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float shootInterval = 2.0f;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private float detectionRadius = 10.0f;

    private bool isActive;



    public void ActivateShooting()
    {
        isActive = true;
        StartCoroutine(Shoot());
    }

    private void Start()
    {
        ActivateShooting();
    }

    private IEnumerator Shoot()
    {
        while (isActive)
        {
            GameObject target = FindNearestEnemy();

            if (target != null)
            {
                ShootAtTarget(target);
            }

            yield return new WaitForSeconds(shootInterval);
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

    private void ShootAtTarget(GameObject target)
    {
        GameObject projectile = Instantiate(projectilePrefab, target.transform.position, Quaternion.identity);
        Destroy(projectile, 5);
        Debug.Log("Shooting Enemy");
    }

    private void OnDestroy()
    {
        isActive = false;
    }
}