using UnityEngine;
public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemyTypes;

    public int basicAmount;
    public int köttbulleAmount;
    public int rangedAmount;
    public int dashAmount;

    public bool doSpawnOverTime;
    [HideInInspector] public bool doContinuousSpawn;
    [HideInInspector] public float continuousSpawnDelay;
    public bool doWaveSpawn;

    int totalEnemies;
    float spawnDelay;

    Camera camera;

    float timer = 0;
    float continuousTimer;
    bool continuousCanSpawn;

    [SerializeField] GameObject EmptyTransform;


    public enum EnemyNames
    {
        basicEnemy, köttbulleEnemy, rangedEnemy, dashEnemy
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        totalEnemies = basicAmount + köttbulleAmount + rangedAmount + dashAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (continuousTimer > 0)
        {
            continuousTimer -= Time.deltaTime;
        }
        else
        {
            if (doContinuousSpawn == true)
            {
                continuousCanSpawn = true;
            }
            continuousTimer = continuousSpawnDelay;
        }
    }

    public void ContinuallySpawn(EnemyNames enemyType)
    {
        if (continuousCanSpawn == true)
        {
            GetRandomSpawn();
            Instantiate(enemyTypes[((int)enemyType)], EmptyTransform.transform.position, Quaternion.identity);
            continuousCanSpawn = false;
        }
    }

    void SpawnWave(EnemyNames enemyType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GetRandomSpawn();
            Instantiate(enemyTypes[((int)enemyType)], EmptyTransform.transform.position, Quaternion.identity);
        }
    }

    void SpawnOverTime(EnemyNames enemyType, int amount, float time)
    {
        if (time < amount)
        {
            spawnDelay = time / amount;
        }
        else
        {
            spawnDelay = amount / time;
        }

        if (timer >= spawnDelay)
        {
            GetRandomSpawn();
            Instantiate(enemyTypes[((int)enemyType)], EmptyTransform.transform.position, Quaternion.identity);
        }
    }


    public void GetRandomSpawn()
    {
        Vector2 spawnLocation;
        float randomY = Random.Range(-2f, 2f);

        if (randomY < -0.2f || randomY > 1.2f)
        {
            float randomX = Random.Range(-1.5f, 2f);

            spawnLocation = camera.ViewportToWorldPoint(new Vector2(randomX, randomY));
        }
        else
        {
            int zeroOrOne = Random.Range(0, 2);

            if (zeroOrOne == 0)
            {
                spawnLocation = camera.ViewportToWorldPoint(new Vector2((Random.Range(-0.5f, -1f)), randomY));
            }
            else
            {
                spawnLocation = camera.ViewportToWorldPoint(new Vector2(Random.Range(1.5f, 2f), randomY));
            }
        }

        EmptyTransform.transform.position = spawnLocation;
    }
}
