using UnityEngine;

public class EndlessWaves : MonoBehaviour
{

    SpawnWaves wavesScript;

    public int waveNumber = 0;

    public bool waveActive;
    public static int numberOfEnemies;
    public int totalEnemies;

    public float difficultyMultiplier;

    public float basicSwarmCost;
    public float k�ttbulleSwarmCost;
    public float rangedCost;
    public float dashCost;

    int basicAmount;
    int basicSwarmAmount;
    int k�ttbulleAmount;
    int rangedAmount;
    int dashAmount;

    int randomSelect;


    public float spawnerPoints;

    int swarmLimit;
    int swarmLimitBasic;

    int playerAmount;

    private void startWave()
    {
        waveActive = true;
    }
    void Start()
    {

        if (FindAnyObjectByType<SaveData>() != null)
        {
            playerAmount = FindAnyObjectByType<SaveData>().playerAmount;
            difficultyMultiplier += playerAmount;
        }

        wavesScript = GetComponent<SpawnWaves>();
        Invoke(nameof(startWave), 1);
    }


    void Update()
    {
        totalEnemies = numberOfEnemies;
        if (numberOfEnemies <= 0 && waveActive == true)
        {
            WaveComplete();
            calculateDifficulty();
        }

        if (numberOfEnemies > 0)
        {
            waveActive = true;
        }
    }

    void WaveComplete()
    {
        wavesScript.startSpawning = false;
        waveActive = false;
        waveNumber++;
        difficultyMultiplier += (0.1f * waveNumber);
        spawnerPoints = difficultyMultiplier;
        swarmLimit = 0;
        swarmLimitBasic = 0;

        swarmLimit = 0 - Mathf.FloorToInt(difficultyMultiplier);
        swarmLimitBasic = 0 - Mathf.FloorToInt(difficultyMultiplier);

    }

    void StartWave()
    {
        wavesScript.basicAmount = basicAmount;
        wavesScript.k�ttbulleAmount = k�ttbulleAmount;
        wavesScript.rangedAmount = rangedAmount;
        wavesScript.dashAmount = dashAmount;
        wavesScript.startSpawning = true;

        basicAmount = 0;
        k�ttbulleAmount = 0;
        rangedAmount = 0;
        dashAmount = 0;
    }

    void calculateDifficulty()
    {

        while (spawnerPoints >= k�ttbulleSwarmCost)
        {
            randomSelect = Random.Range(0, 4);

            if (randomSelect == 0 && spawnerPoints >= basicSwarmCost)
            {
                spawnerPoints -= basicSwarmCost;
                int howManyBasic = 3 - swarmLimitBasic;
                basicAmount += Mathf.Clamp(howManyBasic, 1, 3);

                swarmLimitBasic++;
            }
            else if (randomSelect == 1 && spawnerPoints >= k�ttbulleSwarmCost)
            {
                spawnerPoints -= k�ttbulleSwarmCost;

                int HowManyBulle = 3 - swarmLimit;
                k�ttbulleAmount += Mathf.Clamp(HowManyBulle, 1, 3);
                swarmLimit++;
            }
            else if (randomSelect == 2 && spawnerPoints >= rangedCost)
            {
                spawnerPoints -= rangedCost;
                rangedAmount++;
            }
            else if (randomSelect == 3 && spawnerPoints >= dashCost)
            {
                int rnd = Random.Range(0, 5);

                if (rnd == 0)
                {
                    spawnerPoints -= dashCost;
                    dashAmount++;
                }
            }
        }

        StartWave();

    }
}
