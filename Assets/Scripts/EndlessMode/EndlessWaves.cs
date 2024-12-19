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
    public float köttbulleSwarmCost;
    public float rangedCost;
    public float dashCost;

    int basicAmount;
    int basicSwarmAmount;
    int köttbulleAmount;
    int rangedAmount;
    int dashAmount;

    int randomSelect;


    public float spawnerPoints;

    int swarmLimit;
    int swarmLimitBasic;

    int playerAmount;

    float waveTimeLimit = 60;
    public float waveTimer;

    [HideInInspector] public float startWaveTimer;
    float betweenWavesWaitTime = 10;
    bool canStartWave = false;

    //this is changed in EndlessScaleWithWaves
    [HideInInspector] public bool buffDashEnemy = false;
    private void startWave()
    {
        waveActive = true;
    }
    void Start()
    {
        startWaveTimer = betweenWavesWaitTime;
        difficultyMultiplier = 1f;
        numberOfEnemies = 0;
        spawnerPoints = köttbulleSwarmCost;

        Invoke(nameof(GetPlayAmount), 1);

        wavesScript = GetComponent<SpawnWaves>();
        Invoke(nameof(startWave), 1);
    }

    void GetPlayAmount()
    {
        if (FindAnyObjectByType<SaveData>() != null)
        {
            playerAmount = FindAnyObjectByType<SaveData>().playerAmount;
            Debug.Log(playerAmount);
            difficultyMultiplier += playerAmount;
        }
        else
        {
            Debug.Log("Save data == null");
        }
    }

    void Update()
    {
        if (numberOfEnemies < 0)
        {
            numberOfEnemies = 0;
        }

        if (startWaveTimer > 0)
        {
            canStartWave = false;

            if (numberOfEnemies == 0)
                startWaveTimer -= Time.deltaTime;
        }
        else
        {
            canStartWave = true;
            startWaveTimer = betweenWavesWaitTime;
        }

        totalEnemies = numberOfEnemies;

        if (numberOfEnemies <= 0 && waveActive == true && canStartWave == true)
        {
            WaveComplete();
            calculateDifficulty();
        }

        if (numberOfEnemies > 0)
        {
            waveActive = true;
        }

        if (totalEnemies > 0)
        {
            waveTimer += Time.deltaTime;
        }

        //Backup in case all enemies are dead but next wave has not started
        //should not be needed.
        if (waveTimer >= (waveTimeLimit + difficultyMultiplier * 30))
        {
            if (FindAnyObjectByType<EnemyAttacks>() == null)
            {
                numberOfEnemies = 0;
                waveTimer = 60 + difficultyMultiplier * 30;
            }
            else
            {
                waveTimer = 60 + difficultyMultiplier * 30;
            }
        }
    }

    void WaveComplete()
    {
        waveTimer = 0;
        wavesScript.startSpawning = false;
        waveActive = false;
        waveNumber++;

        //Difficulty for endless mode, after 7 ~ 8 difficulty starts becoming ridiculous, so we slow down the scaling after that point.

        if (difficultyMultiplier < 7)
        {
            difficultyMultiplier += ((0.05f * playerAmount) + (waveNumber * 0.05f));
        }
        else
        {
            difficultyMultiplier += ((0.1f * playerAmount));
        }

        spawnerPoints = difficultyMultiplier;
        swarmLimit = 0;
        swarmLimitBasic = 0;

        swarmLimit = 0 - Mathf.FloorToInt(difficultyMultiplier);
        swarmLimitBasic = 0 - Mathf.FloorToInt(difficultyMultiplier);

        int moduloWave3 = waveNumber % 3;

        if (moduloWave3 == 0)
        {
            dashAmount++;
            buffDashEnemy = true;
        }

    }

    void StartWave()
    {
        wavesScript.basicAmount = basicAmount;
        wavesScript.köttbulleAmount = köttbulleAmount;
        wavesScript.rangedAmount = rangedAmount;
        wavesScript.dashAmount = dashAmount;
        wavesScript.startSpawning = true;

        basicAmount = 0;
        köttbulleAmount = 0;
        rangedAmount = 0;
        dashAmount = 0;
    }

    void calculateDifficulty()
    {

        while (spawnerPoints >= basicSwarmCost)
        {
            randomSelect = Random.Range(0, 4);

            if (randomSelect == 0 && spawnerPoints >= basicSwarmCost)
            {
                spawnerPoints -= basicSwarmCost;
                int howManyBasic = 3 - swarmLimitBasic;
                basicAmount += Mathf.Clamp(howManyBasic, 2, 4);

                swarmLimitBasic++;
            }
            else if (randomSelect == 1 && spawnerPoints >= köttbulleSwarmCost)
            {
                spawnerPoints -= köttbulleSwarmCost;

                int HowManyBulle = 3 - swarmLimit;
                köttbulleAmount += Mathf.Clamp(HowManyBulle, 1, 3);
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
                    basicAmount += 8;

                }
            }
        }

        StartWave();

    }
}
