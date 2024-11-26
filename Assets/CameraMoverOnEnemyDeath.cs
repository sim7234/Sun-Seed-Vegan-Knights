using UnityEngine;

public class CameraMoverOnEnemyDeath : MonoBehaviour
{
    public GameObject[] enemiesStage1; 
    public GameObject[] enemiesStage2; 
    public GameObject[] enemiesStage3; 
    public Transform[] cameraPositions;      
    public float cameraSpeed = 2f;           

    private int currentStage = 0; 
    private bool moveCamera = false; 
    void Update()
    {
        if (currentStage < cameraPositions.Length && AreAllEnemiesDead(GetCurrentEnemies()))
        {
            moveCamera = true; 
        }

        if (moveCamera)
        {
            MoveCameraToTarget();
        }
    }

    void MoveCameraToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, cameraPositions[currentStage].position, cameraSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, cameraPositions[currentStage].position) <= 0.1f)
        {
            transform.position = cameraPositions[currentStage].position;
            moveCamera = false; 
            currentStage++; 
        }
    }

    GameObject[] GetCurrentEnemies()
    {
        switch (currentStage)
        {
            case 0: return enemiesStage1;
            case 1: return enemiesStage2;
            case 2: return enemiesStage3;
            default: return new GameObject[0];
        }
    }

    bool AreAllEnemiesDead(GameObject[] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false; 
            }
        }
        return true; 
    }
}