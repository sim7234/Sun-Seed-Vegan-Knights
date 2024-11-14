using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 10f;
    private float timer;

    [SerializeField]
    private bool spawnOverTime;

    [SerializeField]
    private bool spawnWave;
    [SerializeField]
    private int spawnAmount;

    void Start()
    {
        timer = spawnInterval;
        if(spawnWave == true)
        {
            SpawnWave(spawnAmount);
        }
    }

    void Update()
    {
        if(spawnOverTime == true)
        {
            timer -= Time.deltaTime; 

            if (timer <= 0f)
            {
                SpawnEnemy();
                timer = spawnInterval; 
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnWave(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        MissionMaster.Instance.AddEnemy();
    }
}
