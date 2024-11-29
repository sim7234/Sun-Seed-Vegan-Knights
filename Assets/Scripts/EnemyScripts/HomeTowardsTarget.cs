using UnityEngine;

public class HomeTowardsTarget : MonoBehaviour
{

    isTarget[] potentialTargets = new isTarget[3];
    int finalTarget;
    int totalTargets;
    Rigidbody2D rb;

    Vector3 targetPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        potentialTargets = FindObjectsOfType<isTarget>();
        foreach (isTarget target in potentialTargets)
        {
            totalTargets++;
        }
    }

    // Update is called once per frame
    void Update()
    {
       finalTarget = FindClosestTarget(totalTargets);
       targetPos = potentialTargets[finalTarget].gameObject.transform.position;

        rb.velocity += (new Vector2(Mathf.MoveTowards(rb.transform.position.x, targetPos.x, 0.001f),
            Mathf.MoveTowards(rb.transform.position.y, targetPos.y, 0.001f)));

       // Vector3.RotateTowards(transform.localRotation, targetPos,)
    }

    int FindClosestTarget(int totalTargets)
    {
        float closestTarget = Mathf.Infinity;

        for (int i = 0; i < totalTargets; i++)
        {
            if (potentialTargets[i].gameObject != null)
            {

                Vector3 targetDistence = potentialTargets[i].transform.position - transform.position;
                float targetDistenceSquared = targetDistence.sqrMagnitude;

                if (targetDistenceSquared < closestTarget && potentialTargets[i].gameObject.activeSelf == true)
                {
                    closestTarget = targetDistenceSquared;

                    finalTarget = i;
                }
            }

        }
        return finalTarget;
    }
}
