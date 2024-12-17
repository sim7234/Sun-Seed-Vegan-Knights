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

    //this is changed in EndlessScaleWithWaves
    [HideInInspector] public bool buffDashEnemy = false;
    private void startWave()
    {
        waveActive = true;
    }
    void Start()
    {
        difficultyMultiplier = 1f;
        numberOfEnemies = 0;
        spawnerPoints = köttbulleSwarmCost;

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

        if (totalEnemies > 0)
        {
            waveTimer += Time.deltaTime;
        }
        if (waveTimer >= waveTimeLimit)
        {
            WaveComplete();
            calculateDifficulty();
            waveTimer = 0;
        }

    }

    void WaveComplete()
    {
        waveTimer = 0;
        wavesScript.startSpawning = false;
        waveActive = false;
        waveNumber++;
        difficultyMultiplier += ((0.05f * playerAmount) * waveNumber);
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

        while (spawnerPoints >= köttbulleSwarmCost)
        {
            randomSelect = Random.Range(0, 4);

            if (randomSelect == 0 && spawnerPoints >= basicSwarmCost)
            {
                spawnerPoints -= basicSwarmCost;
                int howManyBasic = 3 - swarmLimitBasic;
                basicAmount += Mathf.Clamp(howManyBasic, 1, 3);

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
                    basicAmount += 2;
                    köttbulleAmount += 2;
                    rangedAmount += 2;
                }
            }
        }

        StartWave();

    }
}
