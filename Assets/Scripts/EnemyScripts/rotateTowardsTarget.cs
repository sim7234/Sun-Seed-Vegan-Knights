using UnityEngine;

public class rotateTowardsTarget : MonoBehaviour
{
    Vector3 targetPos;

    Pathfinding pathfindingScript;
    GameObject rotationPoint;

    [HideInInspector] public bool lockRotation;

    void Start()
    {
        pathfindingScript = GetComponentInParent<Pathfinding>();
        rotationPoint = this.gameObject;
    }

    void Update()
    {
        targetPos = pathfindingScript.targetTransform;

        if (lockRotation == false)
        {
            targetPos.x = targetPos.x - transform.position.x;
            targetPos.y = targetPos.y - transform.position.y;
            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            rotationPoint.transform.rotation = Quaternion.Euler(0, 0, angle - 90 * -1);
        }
    }
}
