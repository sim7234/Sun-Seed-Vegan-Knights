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

    private bool spawnerActive = false;

    [SerializeField]
    private bool talkToMissionMaster = true;

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
        if (!spawnerActive) return;

        if (spawnOverTime)
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

    public void StartSpawner()
    {
        spawnerActive = true;
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
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        if(talkToMissionMaster)
        {
            MissionMaster.Instance.AddEnemy();
        }
        newEnemy.GetComponent<Health>().talkToMissionMaster = talkToMissionMaster;
    }
}
