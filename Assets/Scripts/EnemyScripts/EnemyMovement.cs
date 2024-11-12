using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 5f; 

    private Transform Player; 

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject != null)
        {
            Player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void Update()
    {
        if (Player != null)
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, Player.position, movementSpeed * Time.deltaTime);
        }
    }
}
