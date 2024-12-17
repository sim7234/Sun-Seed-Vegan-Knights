using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    float randomX;
    float randomY;

    bool moveTowardsPlayer;


    GameObject[] playerHolder = new GameObject[10];

    GameObject target;

    int finalTarget;

    int totalTargets;

    float waterSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {

        playerHolder = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerHolder)
        {
            totalTargets++;
        }

        finalTarget = FindClosestTarget(totalTargets);

        target = playerHolder[finalTarget];

        randomX = 0;
        randomY = 0;
        do
        {
            if (randomX == 0)
            {
                randomX = Random.Range(-2f, 3.0f);
            }
            if (randomY == 0)
            {
                randomY = Random.Range(-2f, 3.0f);
            }

        } while (randomX == 0 || randomY == 0);

        DOTween.To(() => transform.position, x => transform.position = x,
            new Vector3(transform.position.x + randomX, transform.position.y + randomY, 0), 0.5f);
    }


    public int FindClosestTarget(int totalTargets)
    {
        float closestTarget = Mathf.Infinity;

        for (int i = 0; i < totalTargets; i++)
        {
            if (playerHolder[i].gameObject != null)
            {

                Vector3 targetDistence = playerHolder[i].transform.position - transform.position;
                float targetDistenceSquared = targetDistence.sqrMagnitude;

                if (targetDistenceSquared < closestTarget)
                {
                    closestTarget = targetDistenceSquared;

                    finalTarget = i;
                }
            }

        }
        return finalTarget;
    }

    private void Update()
    {
        waterSpeed += Time.deltaTime * 0.05f;
        waterSpeed = Mathf.Clamp01(waterSpeed);
        if (target != null)
        {
            if (target.gameObject.activeSelf == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, waterSpeed);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<WaterSystem>() == null)
            return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Test");
            if (other.GetComponent<WaterSystem>().currentWater < other.GetComponent<WaterSystem>().maxWater)
            {
                other.GetComponent<WaterSystem>().currentWater += 0.2f;
                other.GetComponent<WaterSystem>().DisplayDropForTime();
            }

            Score scoreScript = FindAnyObjectByType<Score>();

            if (scoreScript != null)
                scoreScript.score += 1;

            FindAnyObjectByType<WaterSoundController>().amountToPlay++;
        }


        Destroy(gameObject);

    }
}
