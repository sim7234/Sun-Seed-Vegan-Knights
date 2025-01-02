using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] GameObject[] enemies;

    Vector2 mousePos;
    Camera camera;

    bool noPlantCooldown;

    private void Start()
    {
        
        Application.targetFrameRate = 144;
        camera = Camera.main;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = camera.ScreenToWorldPoint(mousePos);

        if (noPlantCooldown)
        {
            PlantSeed[] seedStats = FindObjectsOfType<PlantSeed>();

            foreach (var item in seedStats)
            {
                if (item.gameObject.CompareTag("Player"))
                {
                    item.plantingTimer = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            goToMenu();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            goToHub();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            goToLevel1();
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            goToLevel2();
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            goToSandbox();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            goToEndless();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            killPlayers();
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            resetPathfinding();
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            infiniteHealth();
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            RegainAllStats();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Nocd();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(enemies[0], mousePos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(enemies[1], mousePos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(enemies[2], mousePos, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(enemies[3], mousePos, Quaternion.identity);
        }



        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            PlayerPrefs.SetInt("HasDoneTutorial", 0);
            Menu menu = FindAnyObjectByType<Menu>();
            if (menu != null)
            {
                menu.hasDoneTutorial = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {

            KillAllEnemies();
        }

    }

    

    void KillAllEnemies()
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemyArray)
        {
            if (enemy.GetComponent<Health>()!= null)
            enemy.GetComponent<Health>().TakeDamage(enemy.GetComponent<Health>().maxHealth);
        }

        if (SceneManager.GetActiveScene().name == "EndlessMode" || SceneManager.GetActiveScene().name == "endless wip")
        {
            FindAnyObjectByType<EndlessWaves>().startWaveTimer = 0;
        }
    }
    private void goToLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    void goToEndless()
    {
        SceneManager.LoadScene("EndlessMode");
    }

    void goToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void goToHub()
    {
        SceneManager.LoadScene("True Hub");
    }

    void goToLevel1()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void goToSandbox()
    {
        SceneManager.LoadScene("Sandbox");
    }

    void killPlayers()
    {
        SaveData.Instance.playerAmount = 0;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        CameraSystem cameraScript = FindAnyObjectByType<CameraSystem>();
        cameraScript.playerCount = 0;
        
        cameraScript.FindTargets();
        int i = 0;

        foreach (GameObject p in players)
        {
            i++;
            cameraScript.players.Remove(p);
            Destroy(p);
        }
    }

    void resetPathfinding()
    {
        FindTargets[] getTargets = FindObjectsOfType<FindTargets>();
        foreach (FindTargets target in getTargets)
        {
            target.GetTargets();
        }
    }

    void infiniteHealth()
    {
        Health[] health = FindObjectsOfType<Health>();

        foreach (var item in health)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.maxHealth = 100000;
                item.currentHealth = item.maxHealth;
            }

        }
    }

    private void RegainAllStats()
    {
        PlantSeed[] seedStats = FindObjectsOfType<PlantSeed>();

        foreach (var item in seedStats)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.plantingTimer = 0;
            }
        }
        PlayerWater[] waterStats = FindObjectsOfType<PlayerWater>();

        foreach (var item in waterStats)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.MaxFill();
            }
        }

        Health[] health = FindObjectsOfType<Health>();

        foreach (var item in health)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.currentHealth = item.maxHealth;
            }

        }
    }


    void Nocd()
    {
        PlantSeed[] seedStats = FindObjectsOfType<PlantSeed>();

        foreach (var item in seedStats)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.plantSpeed = 1;
            }
        }

        noPlantCooldown = !noPlantCooldown;

        PlayerWater[] waterStats = FindObjectsOfType<PlayerWater>();

        foreach (var item in waterStats)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.waterGainTime = 0.1f;
            }
        }
    }
}
