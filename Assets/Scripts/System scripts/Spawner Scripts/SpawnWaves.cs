using UnityEngine;

public class SpawnWaves : MonoBehaviour
{
    public bool startSpawning = false;
    [Header("Amount of each type to spawn")]
    public int basicAmount;
    public int köttbulleAmount;
    public int rangedAmount;
    public int dashAmount;

    public bool spawnOnThisObject = false;

    SpawnEnemies spawnEnemies;

    int rnd;

    public bool activeInArea;

    Collider2D collider;
    bool hasSpawned = false;


    void Start()
    {
        if (activeInArea == true && GetComponent<Collider2D>() != null)
        {
            collider = GetComponent<Collider2D>();
        }
        else if (GetComponent<Collider2D>() == null && activeInArea == true)
        {
            Debug.LogError("No Collider2D on " + this.gameObject.name);
        }

        spawnEnemies = Camera.main.GetComponent<SpawnEnemies>();
    }

    void Update()
    {
        if (startSpawning == true && spawnOnThisObject == false)
        {
            spawnEnemies.doWaveSpawn = true;

            if (basicAmount != 0)
            {
                spawnEnemies.SpawnWave(SpawnEnemies.EnemyNames.basicEnemy, basicAmount);
            }

            if (köttbulleAmount != 0)
            {
                spawnEnemies.SpawnWave(SpawnEnemies.EnemyNames.köttbulleEnemy, köttbulleAmount);
            }

            if (rangedAmount != 0)
            {
                spawnEnemies.SpawnWave(SpawnEnemies.EnemyNames.rangedEnemy, rangedAmount);
            }

            if (dashAmount != 0)
            {
                spawnEnemies.SpawnWave(SpawnEnemies.EnemyNames.dashEnemy, dashAmount);
            }
        }
        else if(startSpawning == true && spawnOnThisObject == true)
        {
            if (basicAmount != 0)
            {
                spawnEnemies.SpawnOnLocation(SpawnEnemies.EnemyNames.basicEnemy, basicAmount, this.gameObject.transform.position);
            }

            if (köttbulleAmount != 0)
            {
                spawnEnemies.SpawnOnLocation(SpawnEnemies.EnemyNames.köttbulleEnemy, köttbulleAmount, this.gameObject.transform.position);
            }

            if (rangedAmount != 0)
            {
                spawnEnemies.SpawnOnLocation(SpawnEnemies.EnemyNames.rangedEnemy, rangedAmount, this.gameObject.transform.position);
            }

            if (dashAmount != 0)
            {
                spawnEnemies.SpawnOnLocation(SpawnEnemies.EnemyNames.dashEnemy, dashAmount, this.gameObject.transform.position);
            }
        }

        startSpawning = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activeInArea == true && other.CompareTag("Player") && hasSpawned == false)
        {
            startSpawning = true;
            hasSpawned = true;
        }
    }
}
