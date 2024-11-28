using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.GridBrushBase;

public class LookOnSystem : MonoBehaviour
{
    [HideInInspector]
    private List<GameObject> targetsInRange = new List<GameObject>();

    private GameObject[] targets = new GameObject[5];

    [HideInInspector]
    private GameObject currentTarget;

    private PlayerMovement playerMovement;

    [SerializeField]
    private float minDistanceForAttack;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void AttackClosestEnemy()
    {
        TargetClosestEnemy();
        if(currentTarget != null)
        {
            if(targetsInRange.Count > 0 && Vector3.Distance(currentTarget.transform.position, transform.position) < minDistanceForAttack)
            {
                GetComponent<PlayerAttack>().DashAttack(); 
                StartCoroutine(TurnOfLockOn());
            }
        }
    }
    public void TargetClosestEnemy()
    {
        GetTargets();
        if (targetsInRange.Count > 0)
        {
            FindClosestTarget();

            
            if (currentTarget != null)
            {
                float angle = Mathf.Atan2(-(transform.position.y - currentTarget.transform.position.y), -(transform.position.x - currentTarget.transform.position.x)) * Mathf.Rad2Deg;
                playerMovement.directionIndicator.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }
        else
        {
            playerMovement.lockOn = false;
        }
    }
    public void GetTargets()
    {
        Debug.Log("Get");
        targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject target in targets)
        {
            if(!targetsInRange.Contains(target))
            {
                targetsInRange.Add(target);
            }
        }
        for (int i = 0; i < targetsInRange.Count; i++)
        {
            if (targetsInRange[i] == null)
            {
                targetsInRange.RemoveAt(i);
            }
        }
    }
    private void FindClosestTarget()
    {
        float closestTarget = Mathf.Infinity;

        for (int i = 0; i < targetsInRange.Count; i++)
        {
            if (targetsInRange[i].gameObject != null)
            {
                Vector3 targetDistence = targetsInRange[i].transform.position - transform.position;
                float targetDistenceSquared = targetDistence.sqrMagnitude;

                if (targetDistenceSquared < closestTarget)
                {
                    closestTarget = targetDistenceSquared;

                    currentTarget = targetsInRange[i];
                }
            }

        }
    }
    private IEnumerator TurnOfLockOn()
    {
        playerMovement.lockOn = true;
        yield return new WaitForSeconds(0.3f);
        playerMovement.lockOn = false;
    }
}
