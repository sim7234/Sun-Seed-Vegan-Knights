using UnityEngine;

public class ContinuallySpawn : MonoBehaviour
{
    public bool startSpawning = false;
    public float spawnDelay;

    SpawnEnemies spawnEnemies;

    [Header("What To Spawn")]
    public bool basicEnemy;
    public bool köttbulleEnemy;
    public bool rangedEnemy;
    public bool dashEnemy;

    bool[] boolActiveAmount = new bool[4];

    int activeAmount = 0;

    int rnd;

    void Start()
    {
        spawnEnemies = Camera.main.GetComponent<SpawnEnemies>();
    }

    void Update()
    {
        activeAmount = 0;
        if (basicEnemy)
            activeAmount++;

        if (köttbulleEnemy)
            activeAmount++;

        if (rangedEnemy)
            activeAmount++;

        if (dashEnemy)
            activeAmount++;


        if (startSpawning == true)
        {
            spawnEnemies.doContinuousSpawn = true;
            spawnEnemies.continuousSpawnDelay = spawnDelay;


            rnd = Random.Range(0, activeAmount);

            if (basicEnemy && rnd == 0)
                spawnEnemies.ContinuallySpawn(SpawnEnemies.EnemyNames.basicEnemy);

            if (köttbulleEnemy && rnd == 1)
                spawnEnemies.ContinuallySpawn(SpawnEnemies.EnemyNames.köttbulleEnemy);

            if (rangedEnemy && rnd == 2)
                spawnEnemies.ContinuallySpawn(SpawnEnemies.EnemyNames.rangedEnemy);

            if (dashEnemy && rnd == 3)
                spawnEnemies.ContinuallySpawn(SpawnEnemies.EnemyNames.dashEnemy);
        }
    }
}
