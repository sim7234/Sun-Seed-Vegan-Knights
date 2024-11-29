using UnityEngine;

public class CameraMoverTutorial : MonoBehaviour
{
    public Transform[] cameraPoints; 
    public float transitionSpeed = 2f;
    private int currentPointIndex = 0; 
    private bool isMoving = false; 

    public Health enemyHealth; 

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveCameraToNextPoint();
        }

        if (enemyHealth != null && enemyHealth.GetCurrentHealth() <= 0 && !isMoving)
        {
            MoveToNextPoint();
        }
    }

    public void MoveToNextPoint()
    {
        if (currentPointIndex < cameraPoints.Length - 1)
        {
            currentPointIndex++;
            isMoving = true;
        }
    }

    private void MoveCameraToNextPoint()
    {
        Transform targetPoint = cameraPoints[currentPointIndex];
        transform.position = Vector3.Lerp(transform.position, targetPoint.position, Time.deltaTime * transitionSpeed);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            transform.position = targetPoint.position;
            isMoving = false;
        }
    }
}