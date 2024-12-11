using UnityEngine;

public class ContinuallySpawn : MonoBehaviour
{
    [HideInInspector] public bool startSpawning = false;
    public float spawnDelay;

    SpawnEnemies spawnEnemies;

    [Header("What To Spawn")]
    public bool basicEnemy;
    public bool köttbulleEnemy;
    public bool rangedEnemy;
    public bool dashEnemy;

    int rnd;

    public bool activeInArea;
    Collider2D collider;
    bool hasSpawned = false;
    void Start()
    {
        spawnEnemies = Camera.main.GetComponent<SpawnEnemies>();
        if (activeInArea == true && GetComponent<Collider2D>() != null)
        {
            collider = GetComponent<Collider2D>();
        }
        else if (GetComponent<Collider2D>() == null && activeInArea == true)
        {
            Debug.LogError("No Collider2D on " + this.gameObject.name);
        }
    }

    void Update()
    {
        if (startSpawning == true)
        {
            spawnEnemies.doContinuousSpawn = true;
            spawnEnemies.continuousSpawnDelay = spawnDelay;

            rnd = Random.Range(0, 4);

            if (basicEnemy && rnd == 0)
            {
                spawnEnemies.ContinuallySpawn(SpawnEnemies.EnemyNames.basicEnemy);
            }
            else if (köttbulleEnemy && rnd == 1)
            {
                spawnEnemies.ContinuallySpawn(SpawnEnemies.EnemyNames.köttbulleEnemy);
            }
            else if (rangedEnemy && rnd == 2)
            {
                spawnEnemies.ContinuallySpawn(SpawnEnemies.EnemyNames.rangedEnemy);
            }
            else if (dashEnemy && rnd == 3)
            {
                spawnEnemies.ContinuallySpawn(SpawnEnemies.EnemyNames.dashEnemy);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activeInArea == true && other.CompareTag("Player") && hasSpawned == false)
        {
            startSpawning = true;
            hasSpawned = true;
        }

        if (other.GetComponent<StopSpawning>() != null)
        {
            Debug.Log("test");
            startSpawning = false;
        }   
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (activeInArea == true && other.CompareTag("Player") && hasSpawned == true)
        {
            startSpawning = false;
            hasSpawned = false;
        }

    }
}
